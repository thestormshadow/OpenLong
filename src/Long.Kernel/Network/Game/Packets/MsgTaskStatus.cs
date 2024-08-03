using Long.Database.Entities;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgTaskStatus : MsgBase<GameClient>
    {
        public List<TaskItemStruct> Tasks = new();
        public TaskStatusMode Mode { get; set; }
        public ushort Amount { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (TaskStatusMode)reader.ReadUInt16();
            Amount = reader.ReadUInt16();
            for (var i = 0; i < Amount; i++)
            {
                var item = new TaskItemStruct
                {
                    Identity = reader.ReadInt32(),
                    Status = (TaskItemStatus)reader.ReadInt32(),
                    Time = reader.ReadInt32()
                };
                Tasks.Add(item);
            }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTaskStatus);
            writer.Write((ushort)Mode); // 4
            writer.Write(Amount = (ushort)Tasks.Count); // 6
            foreach (TaskItemStruct task in Tasks)
            {
                writer.Write(task.Identity); // 8
                writer.Write((int)task.Status); // 12
                writer.Write(task.Time); // 16
            }
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (Mode == TaskStatusMode.Update)
            {
                foreach (var item in Tasks)
                {
                    DbTaskDetail taskDetail = user.TaskDetail.QueryTaskData((uint)item.Identity);
                    if (taskDetail != null)
                    {
                        if (taskDetail.CompleteFlag >= 1)
                        {
                            item.Status = TaskItemStatus.Done;
                        }
                        else
                        {
                            item.Status = TaskItemStatus.Accepted;
                        }

                        item.Time = (int)taskDetail.MaxAccumulateTimes;
                    }
                    else
                    {
                        item.Status = TaskItemStatus.Available;
                    }
                }
                await client.SendAsync(this);
            }
            else if (Mode == TaskStatusMode.Add)
            {
                TaskItemStruct taskItemStruct = Tasks.FirstOrDefault();
                if (taskItemStruct == null)
                {
                    return;
                }

                DbTaskDetail taskDetail = user.TaskDetail.QueryTaskData((uint)taskItemStruct.Identity);
                if (taskDetail == null)
                {
                    await user.TaskDetail.CreateNewAsync((uint)taskItemStruct.Identity);
                }
            }
        }

        public class TaskItemStruct
        {
            public int Identity { get; set; }
            public TaskItemStatus Status { get; set; }
            public int Time { get; set; }
        }

        public enum TaskItemStatus : byte
        {
            Accepted = 0,
            Done = 1,
            Available = 2,
            TaskFail = 5
        }

        public enum TaskStatusMode : byte
        {
            None = 0,
            Add = 1,
            Remove = 2,
            Update = 3,
            Finish = 4,
            TaskFail = 6,
            Quit = 8
        }
    }
}
