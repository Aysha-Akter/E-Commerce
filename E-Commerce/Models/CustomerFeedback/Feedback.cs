using E_Commerce.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models.CustomerFeedback
{
    public class Feedback
    {
        [Key]
        public int ID { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Message { get; private set; }

        public bool IsRead { get; private set; }

        public Feedback()
        {
            //ForBlankObjectCreating
        }

        public Feedback(FeedbackViewModel viewModel)
        {
            Name = viewModel.Name;
            Email = viewModel.Email;
            Phone = viewModel.Phone;
            Message = viewModel.Message;
        }

        public void Read(bool isRead)
        {
            IsRead = isRead;
        }
    }
}
