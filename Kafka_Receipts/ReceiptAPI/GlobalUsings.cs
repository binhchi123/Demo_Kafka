global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;
global using System.Text.Json;

global using Confluent.Kafka;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;

global using ReceiptAPI.Application.Models;
global using ReceiptAPI.Application.Repository;
global using ReceiptAPI.Application.Service;
global using ReceiptAPI.Database;
global using ReceiptAPI.DTOs;
global using ReceiptAPI.Repository;
global using ReceiptAPI.Service;
global using ReceiptAPI.Producer;