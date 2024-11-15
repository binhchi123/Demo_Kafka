global using System.ComponentModel.DataAnnotations;
global using System.Text;
global using System.Text.Json;

global using Confluent.Kafka;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Manonero.MessageBus.Kafka.Abstractions;
global using Manonero.MessageBus.Kafka.Settings;

global using Kafka_Employee_API.Core.Database;
global using Kafka_Employee_API.Core.DTOs;
global using Kafka_Employee_API.Core.InMemorys;
global using Kafka_Employee_API.Core.Models;
global using Kafka_Employee_API.Core.IService;
global using Kafka_Employee_API.Core.Seedings;

global using Kafka_Employee_API.BackgroundTasks;
global using Kafka_Employee_API.Extensions;
global using Kafka_Employee_API.Producer;
global using Kafka_Employee_API.Requests;
global using Kafka_Employee_API.Service;
global using Kafka_Employee_API.Setting;