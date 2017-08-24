using System;

namespace IOSBackupReader
{
    public class Mode
    {
        public UInt32 st_mode { get; set; }
        public Mode(UInt32 st_mode)
        {
            this.st_mode = st_mode;
        }
        public void Set(ModeFlag flag)
        {
            this.st_mode |= (UInt32)flag;
        }
        public void Clear(ModeFlag flag)
        {
            this.st_mode &= ~(UInt32)flag;
        }
        public void Toggle(ModeFlag flag)
        {
            this.st_mode ^= (UInt32)flag;
        }
        public bool Check(ModeFlag flag)
        {
            return (this.st_mode & (UInt32)flag) != 0;
        }
        public static implicit operator UInt32(Mode mode)
        {
            return mode.st_mode;
        }
        public static implicit operator Mode(UInt32 st_mode)
        {
            return new Mode(st_mode);
        }
    }
    public enum ModeFlag : UInt32
    {
        S_IFMT = 0170000,   // bit mask for the file type bit fields
        S_IFSOCK = 0140000, // socket
        S_IFLNK = 0120000,  // symbolic link
        S_IFREG = 0100000,  // regular file
        S_IFBLK = 0060000,  // block device
        S_IFDIR = 0040000,  // directory
        S_IFCHR = 0020000,  // character device
        S_IFIFO = 0010000,  // FIFO
        S_ISUID = 0004000,  // set-user-ID bit
        S_ISGID = 0002000,  // set-group-ID bit(see below)
        S_ISVTX = 0001000,  // sticky bit(see below)
        S_IRWXU = 00700,    // mask for file owner permissions
        S_IRUSR = 00400,    // owner has read permission
        S_IWUSR = 00200,    // owner has write permission
        S_IXUSR = 00100,    // owner has execute permission
        S_IRWXG = 00070,    // mask for group permissions
        S_IRGRP = 00040,    // group has read permission
        S_IWGRP = 00020,    // group has write permission
        S_IXGRP = 00010,    // group has execute permission
        S_IRWXO = 00007,    // mask for permissions for others(not in group)
        S_IROTH = 00004,    // others have read permission
        S_IWOTH = 00002,    // others have write permission
        S_IXOTH = 00001,    // others have execute permission
    }
}
