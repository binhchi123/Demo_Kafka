global using System.ComponentModel.DataAnnotations;
global using System.Text;
global using System.Text.Json;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Confluent.Kafka;
global using Manonero.MessageBus.Kafka.Settings;
global using Manonero.MessageBus.Kafka.Abstractions;

global using Kafka_Student_API.Core.Database;
global using Kafka_Student_API.Core.DTOs;
global using Kafka_Student_API.Core.InMemorys;
global using Kafka_Student_API.Core.IService;
global using Kafka_Student_API.Core.Models;
global using Kafka_Student_API.Core.Seedings;

global using Kafka_Student_API.BackgroundTasks;
global using Kafka_Student_API.Extensions;
global using Kafka_Student_API.Producer;
global using Kafka_Student_API.Requests;
global using Kafka_Student_API.Service;
global using Kafka_Student_API.Settings;