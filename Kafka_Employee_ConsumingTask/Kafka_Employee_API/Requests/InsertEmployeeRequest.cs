﻿namespace Kafka_Employee_API.Requests
{
    public class InsertEmployeeRequest
    {
        public int      EmployeeId  { get; set; }
        public string   Name        { get; set; }
        public DateTime Birthday    { get; set; }
        public string   PhoneNumber { get; set; }
        public string   Address     { get; set; }
        public string   Email       { get; set; }
    }
}
