using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class InterestQuestion : ReferenceData
    {
        public int questionID { get; private set; }
        public int interestCategoryID { get; private set; }
        public string question { get; private set; }

        public InterestQuestion(int questionID, int interestCategoryID, string question)
        {
            this.questionID = questionID;
            this.interestCategoryID = interestCategoryID;
            this.question = question;
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
