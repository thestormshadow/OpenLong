using Long.Network.Sockets;
using Org.BouncyCastle.Math;
using System.Security.Cryptography;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiHandshake<T> : MsgBase<T> where T : TcpServerActor
	{
		protected MsgAiHandshake()
		{

		}

		protected MsgAiHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
		{
			StartPadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32));
			MiddlePadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32));
			PublicKey = publicKey.ToByteArrayUnsigned();
			Modulus = modulus.ToByteArrayUnsigned();
			EncryptIV = eIv;
			DecryptIV = dIv;
			FinalPadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32));
			FinalTrash = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32));
		}

		public byte[] StartPadding { get; set; }
		public byte[] MiddlePadding { get; set; }
		public byte[] PublicKey { get; set; }
		public byte[] Modulus { get; set; }
		public byte[] EncryptIV { get; set; }
		public byte[] DecryptIV { get; set; }
		public byte[] FinalPadding { get; set; }
		public byte[] FinalTrash { get; set; }

		/// <inheritdoc />
		public override byte[] Encode()
		{
			using PacketWriter writer = new PacketWriter();
			writer.Write((byte)StartPadding.Length);
			writer.Write(StartPadding);
			writer.Write((byte)MiddlePadding.Length);
			writer.Write(MiddlePadding);
			writer.Write((ushort)PublicKey.Length);
			writer.Write(PublicKey);
			writer.Write((ushort)Modulus.Length);
			writer.Write(Modulus);
			writer.Write((byte)EncryptIV.Length);
			writer.Write(EncryptIV);
			writer.Write((byte)DecryptIV.Length);
			writer.Write(DecryptIV);
			writer.Write((byte)FinalPadding.Length);
			writer.Write(FinalPadding);
			writer.Write((byte)FinalTrash.Length);
			writer.Write(FinalTrash);
			return writer.ToArray();
		}

		/// <inheritdoc />
		public override void Decode(byte[] bytes)
		{
			using PacketReader reader = new PacketReader(bytes);
			Length = reader.ReadUInt16();
			int startPadding = reader.ReadByte();
			StartPadding = reader.ReadBytes(startPadding);
			int middlePadding = reader.ReadByte();
			MiddlePadding = reader.ReadBytes(middlePadding);
			int publicKey = reader.ReadUInt16();
			PublicKey = reader.ReadBytes(publicKey);
			int modulus = reader.ReadUInt16();
			Modulus = reader.ReadBytes(modulus);
			int encryptIv = reader.ReadByte();
			EncryptIV = reader.ReadBytes(encryptIv);
			int decryptIv = reader.ReadByte();
			DecryptIV = reader.ReadBytes(decryptIv);
			int finalPadding = reader.ReadByte();
			FinalPadding = reader.ReadBytes(finalPadding);
			int finalTrash = reader.ReadByte();
			FinalTrash = reader.ReadBytes(finalTrash);
		}
	}
}
