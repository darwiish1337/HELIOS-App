﻿global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using WebApimyServices.Models;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using WebApimyServices.Data;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.AspNetCore.Mvc;
global using WebApimyServices.Dto;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;
global using MimeKit;
global using MailKit.Net.Smtp;
global using WebApimyServices.Services;
global using Microsoft.Extensions.Options;
global using MailKit.Security;
global using System.Text.Encodings.Web;
global using WebApimyServices.Contants;
global using WebApimyServices.ExtensionMethods;
global using WebApimyServices.Configuration;
global using System.Security.Cryptography;
global using System.Text.Json.Serialization;
global using WebApimyServices.Attributes;
global using WebApimyServices.Configurations;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using WebApimyServices.Seeding;
global using WebApimyServices.Helpers;
global using System.Reflection;
global using Hangfire;
global using Hangfire.SqlServer;
global using WebApimyServices.Hangfire;
global using WebApimyServices.Middelwares;
global using Microsoft.AspNetCore.Mvc.Filters;