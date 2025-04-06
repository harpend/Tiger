using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;
using Tiger.Frame.Arch;

namespace Tiger.Translate
{
    public class Level
    {
        public Level parent;
        public IFrame frame;
        public List<Access> formals;
        public Level(Level parent, IFrame frame)
        {
            this.parent = parent;
            this.frame = frame;
            this.formals = new List<Access>();
        }
    }
    public class Access
    {
        public Level level;
        public IAccess access;
        public Access(Level level, IAccess access)
        {
            this.level = level;
            this.access = access;
        }
    }

    public class Translate
    {
        public static IFrame curFrame;
        public static string target = "x86_64";
        
        public static Level NewLevel(Level parent, Label name, List<bool> formals)
        {
            IFrame frame = null;
            if (target == "x86_64")
            {
                frame = parent.frame.NewFrame(name, formals);
            }

            Level level = new Level(parent, frame);
            List<IAccess> iAccessList = frame.Formals();
            List<Access> accessList = new List<Access>();
            iAccessList.ForEach(ia => { accessList.Add(new Access(level, ia)); });
            level.formals = accessList;
            return level;
        }

        public static Level Outermost()
        {
            return outermost;
        }

        public static List<Access> Formals(Level level)
        {
            return level.formals;
        }

        public static Access AllocLocal(Level level, bool escape)
        {
            IAccess iAccess = level.frame.AllocLocal(escape);
            return new Access(level, iAccess);
        }
    }
}
