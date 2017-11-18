﻿using Intersect.Migration.UpgradeInstructions.Upgrade_10.Intersect_Convert_Lib.Memory;

namespace Intersect.Migration.UpgradeInstructions.Upgrade_10.Intersect_Convert_Lib.Network.Packets.Reflectable
{
    public class BinaryPacket : AbstractPacket
    {
        public ByteBuffer Buffer;

        public BinaryPacket(IConnection connection)
            : base(connection, PacketCode.BinaryPacket)
        {
        }

        public override int EstimatedSize => Buffer?.Length() + sizeof(int) ?? sizeof(int);

        public override bool Read(ref IBuffer buffer)
        {
            if (!base.Read(ref buffer)) return false;

            if (!buffer.Read(out byte[] bytes)) return false;

            Buffer = new ByteBuffer();
            Buffer.WriteBytes(bytes);

            return true;
        }

        public override bool Write(ref IBuffer buffer)
        {
            if (!base.Write(ref buffer)) return false;

            if (Buffer == null)
            {
                buffer.Write(0);
            }
            else
            {
                buffer.Write(Buffer.ToArray());
            }

            return true;
        }

        public override void Dispose()
        {
            Buffer?.Dispose();
        }
    }
}