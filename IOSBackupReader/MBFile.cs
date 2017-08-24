using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSBackupReader
{
    public class MBFile
    {
        public String FileID;
        public String Domain;
        public String RelativePath;
        public String Target;
        public UInt32 Mode;
        public UInt64 InodeNumber;
        public Int32 UserID;
        public Int32 GroupID;
        public UInt64 LastModified;
        public UInt64 LastStatusChange;
        public UInt64 Birth;
        public UInt64 Size;
        public UInt32 ProtectionClass;
    }
}
