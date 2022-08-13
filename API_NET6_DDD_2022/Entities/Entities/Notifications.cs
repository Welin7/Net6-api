﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifications
    {
        public Notifications()
        {
            NotificationsList = new List<Notifications>();
        }

        [NotMapped]
        public string PropertyName { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public List<Notifications> NotificationsList { get; set; } 
        
        public bool ValidateStringProperty(string value, string propertyName)
        {
            if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
            {
                NotificationsList.Add(new Notifications
                {
                    Message = "Required Field",
                    PropertyName = propertyName
                });

                return false;
            }

            return true;
        }

        public bool ValidateIntProperty(int value, string propertyName)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(propertyName))
            {
                NotificationsList.Add(new Notifications
                {
                    Message = "Required Field",
                    PropertyName = propertyName
                });

                return false;
            }

            return true;
        }
    }
}
