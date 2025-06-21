﻿global using System.Net.Http.Headers;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using Asp.Versioning;
global using Asp.Versioning.ApiExplorer;

global using AutoLot.Api.ApiVersionSupport;
global using AutoLot.Api.Controllers.Base;
global using AutoLot.Api.Filters;
global using AutoLot.Api.Security;
global using AutoLot.Api.Swagger;
global using AutoLot.Api.Swagger.Models;
global using AutoLot.Dals.EfStructures;
global using AutoLot.Dals.Exceptions;
global using AutoLot.Dals.Initialization;
global using AutoLot.Dals.Repos.Base;
global using AutoLot.Dals.Repos.Interfaces;
global using AutoLot.Models.Entities;
global using AutoLot.Models.Entities.Base;
global using AutoLot.Services.DataServices;
global using AutoLot.Services.Logging.Configuration;
global using AutoLot.Services.Logging.Interfaces;
global using AutoLot.Services.ViewModels;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;

global using Swashbuckle.AspNetCore.Annotations;
global using Swashbuckle.AspNetCore.SwaggerGen;
