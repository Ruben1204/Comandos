using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandos.ViewModel
{
    public  class PersonViewModel: BaseViewModel
    {
        string name;
        double age;
        string skills;

        public string Name
        {
            set { SetProperty(ref name, value);}
            get { return name; }
        }

        public double Age
        {
            set { SetProperty(ref age, value); }
            get { return age; }
        }

        public string Skills
        {
            set { SetProperty(ref skills, value); }
            get { return skills; }
        }

        public override string ToString()
        {
            return Name + ", age " + Age;
        }
    }
}
