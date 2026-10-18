using FuturePathGroup10.Interest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class DegreeProgramme
    {
        private int facultyID;
        private int degreeID;

        public Collection<InterestCategory> categories { get; private set; } = new Collection<InterestCategory>();

        public DegreeProgramme getDegreeProgramme()
        {
            throw new NotImplementedException();
        }

        public void addCategory(InterestCategory category)
        {
            if (!categories.Contains(category))
            {
                categories.Add(category);
            }
        }
    }
}
