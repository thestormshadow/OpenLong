using Long.Network.Sockets;
using Org.BouncyCastle.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.Threading.AsyncReaderWriterLock;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Long.Network.Packets
{
	public abstract class MsgProtoGenericBase<TActor, TData> : MsgBase<TActor> where TActor : TcpServerActor
	{
		public MsgProtoGenericBase(PacketType packetType)
		{
			Type = packetType;
		}

		public TData Data { get; set; }

		public override byte[] Encode()
		{
			using PacketWriter writer = new();
			writer.Write((ushort)Type);
			var propiedades = Data.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(ContractProperty)) != null).OrderBy(x => ((ContractProperty)x.GetCustomAttribute(typeof(ContractProperty))).Tag).ToList();
			propiedades.AddRange(Data.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(ContractProperty)) == null));
			foreach (var propiedad in propiedades)
			{
				if (propiedad.PropertyType == typeof(byte) || propiedad.PropertyType == typeof(Byte))				
					writer.Write((byte)propiedad.GetValue(Data));
				if (propiedad.PropertyType == typeof(byte[]) || propiedad.PropertyType == typeof(Byte[]))
					writer.Write((byte[])propiedad.GetValue(Data));
				else if (propiedad.PropertyType == typeof(ushort) || propiedad.PropertyType == typeof(short) || propiedad.PropertyType == typeof(Int16))
					writer.Write((ushort)propiedad.GetValue(Data));
				else if (propiedad.PropertyType == typeof(uint) || propiedad.PropertyType == typeof(int) || propiedad.PropertyType == typeof(Int32))				
					writer.Write((uint)propiedad.GetValue(Data));
				else if (propiedad.PropertyType == typeof(ulong) || propiedad.PropertyType == typeof(long) || propiedad.PropertyType == typeof(Int64))
					writer.Write((uint)propiedad.GetValue(Data));
				else if (propiedad.PropertyType == typeof(string))
				{
					var decore = propiedad.GetCustomAttributes<ContractProperty>()?.FirstOrDefault();
					if (decore != null)
						writer.Write(propiedad.GetValue(Data).ToString(), decore.FixLength);
					else
						writer.Write(propiedad.GetValue(Data).ToString());
				}
			}
			return writer.ToArray();
		}

		public override void Decode(byte[] bytes)
		{
			using PacketReader reader = new(bytes);
			Length = reader.ReadUInt16();
			Type = (PacketType)reader.ReadUInt16();

			var propiedades = typeof(TData).GetProperties();

			Data = Activator.CreateInstance<TData>();

			foreach (var propiedad in propiedades)
			{
				if (propiedad.PropertyType == typeof(byte) || propiedad.PropertyType == typeof(Byte))
					propiedad.SetValue(Data, reader.ReadByte());
				if (propiedad.PropertyType == typeof(byte[]) || propiedad.PropertyType == typeof(Byte[]))
				{
					var decore = propiedad.GetCustomAttributes<ContractProperty>()?.FirstOrDefault();
					if (decore != null)
						propiedad.SetValue(Data, reader.ReadBytes(decore.FixLength));
					else
						propiedad.SetValue(Data, reader.ReadBytes(1));

				}
				else if (propiedad.PropertyType == typeof(ushort) || propiedad.PropertyType == typeof(short) || propiedad.PropertyType == typeof(Int16))
					propiedad.SetValue(Data, reader.ReadUInt16());
				else if (propiedad.PropertyType == typeof(uint) || propiedad.PropertyType == typeof(int) || propiedad.PropertyType == typeof(Int32))
					propiedad.SetValue(Data, reader.ReadUInt32());
				else if (propiedad.PropertyType == typeof(ulong) || propiedad.PropertyType == typeof(long) || propiedad.PropertyType == typeof(Int64))
					propiedad.SetValue(Data, reader.ReadUInt64());
				else if (propiedad.PropertyType == typeof(string))
				{
					propiedad.SetValue(Data, reader.ReadString());
				}
			}
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
		AllowMultiple = false, Inherited = true)]
	public class ContractProperty : Attribute

	{
		/// <summary>
		/// fixLength = 0 is desactivate, is only for prop length
		/// </summary>
		public ContractProperty(int tag = 0, int fixLength = 0)
		{ 
			this.tag = tag;
			this.fixLength = fixLength;
		}

		/// <summary>
		/// Gets the unique tag used to identify this member within the type.
		/// </summary>
		public int Tag { get { return tag; } }
		private int tag;
		internal void Rebase(int tag) { this.tag = tag; }
		/// <summary>
		/// Gets or sets the original name defined in the .proto; not used
		/// during serialization.
		/// </summary>
		public int FixLength { get { return fixLength; } }
		private int fixLength;

	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,
		AllowMultiple = false, Inherited = false)]
	public class ContractAttribute : Attribute
	{
	}
}
