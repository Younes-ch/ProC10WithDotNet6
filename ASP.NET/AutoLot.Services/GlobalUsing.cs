global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Data;
global using System.Net.Http.Json;
global using System.Reflection;
global using System.Runtime.CompilerServices;

global using AutoLot.Dals.Repos;
global using AutoLot.Dals.Repos.Base;
global using AutoLot.Dals.Repos.Interfaces;
global using AutoLot.Models.Entities;
global using AutoLot.Models.Entities.Base;
global using AutoLot.Services.ApiWrapper.Base;
global using AutoLot.Services.ApiWrapper.Interfaces;
global using AutoLot.Services.ApiWrapper.Models;
global using AutoLot.Services.DataServices.Api;
global using AutoLot.Services.DataServices.Api.Base;
global using AutoLot.Services.DataServices.Dal;
global using AutoLot.Services.DataServices.Dal.Base;
global using AutoLot.Services.DataServices.Interfaces;
global using AutoLot.Services.Logging.Interfaces;
global using AutoLot.Services.Logging.Settings;
global using AutoLot.Services.Validation;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;

global using Serilog.Context;
global using Serilog.Sinks.MSSqlServer;
