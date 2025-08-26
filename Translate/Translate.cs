using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Tiger.Frame;
using Tiger.Frame.Arch;
using Tiger.Frame.Temp;

namespace Tiger.Translate
{
    public static class Translate
    {
        public static Level Outermost = new Level(null, null, null);
        public static Level NewLevel(Level parent, Label name, List<bool> formals) {
            List<bool> bools = new List<bool>();

            // add the static link to the front
            bools.Add(true); 
            foreach (bool f in formals)
            {
                bools.Add(f);
            }

            IFrame frame = new x64Frame(new Label(), bools);
            return new Level(frame, parent, bools);
        } 

        public static List<Access> Formals(Level l)
        {
            List<IAccess> formals = l.Frame.GetFormals();
            List<Access> accessList = new List<Access>();
            foreach (IAccess f in formals)
            {
                accessList.Add(new Access(l, f));
            }

            return accessList;
        }

        public static Access AllocLocal(bool escaped, Level l)
        {
            IAccess access = l.Frame.AllocLocal(escaped);
            return new Access(l, access);
        }
    }
}
