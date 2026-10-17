using System;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class InterestQuestion : ReferenceData
    {
        public int questionID { get; private set; }
        public int interestCategoryID { get; private set; }
        public string question { get; private set; }
        public Collection<InterestCategory> categories { get; private set; }

        public InterestQuestion(int questionID, int interestCategoryID, string question)
        {
            this.questionID = questionID;
            this.interestCategoryID = interestCategoryID;
            this.question = question;
            categories = new Collection<InterestCategory>();
        }

        public void addCategory(InterestCategory category)
        {
            categories.Add(category);
        }

        public void responseToCategory(string response)
        {
            throw new NotImplementedException();
        }

        public override void add()
        {
            throw new NotImplementedException();
        }

        public override void deactivate()
        {
            throw new NotImplementedException();
        }

        public override void update()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj is InterestQuestion)
            {
                InterestQuestion other = (InterestQuestion) obj;
                return questionID == other.questionID;
            }
            else
            {
                return false;
            }
        }
    }
}
