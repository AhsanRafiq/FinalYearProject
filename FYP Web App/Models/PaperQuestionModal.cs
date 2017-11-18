using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class PaperQuestionModal
    {
        public int Id { get; set; }
        public int PaperId { get; set; }

        public string Text { get; set; }
        //Can be null
        public Byte[] QuestionPicture { get; set; }

        public string QuestionImage { get; set; }

        public string Resources { get; set; }

        public string Marks { get; set; }

        public QuestionMCQ OptionOne { get; set; }
        public QuestionMCQ OptionTwo { get; set; }
        public QuestionMCQ OptionThree { get; set; }
        public QuestionMCQ OptionFour { get; set; }

        //Just for storing the index of question from List
        public int Index { get; set; }

     public override bool Equals(object obj)
        {
            PaperQuestionModal paperQuestionModal = (PaperQuestionModal)obj;
            if (this.Text.Equals(paperQuestionModal.Text))
            {

                if (this.Resources.Equals(paperQuestionModal.Resources))
                {
                    if (this.OptionFour.Text.Equals(paperQuestionModal.OptionFour.Text) &&
                        this.OptionFour.Correct == paperQuestionModal.OptionFour.Correct)
                    {
                        if (this.OptionThree.Text.Equals(paperQuestionModal.OptionThree.Text) &&
                            this.OptionThree.Correct == paperQuestionModal.OptionThree.Correct)
                        {
                            if (this.OptionTwo.Text.Equals(paperQuestionModal.OptionTwo.Text) &&
                                this.OptionTwo.Correct == paperQuestionModal.OptionTwo.Correct)
                            {
                                if (this.OptionOne.Text.Equals(paperQuestionModal.OptionOne.Text) &&
                                    this.OptionOne.Correct == paperQuestionModal.OptionOne.Correct)
                                {
                                    return true;

                                }
                            }
                        }

                    }
                }
            }

            return false;
        }
       
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Resources != null ? Resources.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OptionOne != null ? OptionOne.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OptionTwo != null ? OptionTwo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OptionThree != null ? OptionThree.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OptionFour != null ? OptionFour.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}