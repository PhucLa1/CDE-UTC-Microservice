﻿namespace BuildingBlocks
{
    public class Setting
    {
        public const string AUTH_HOST = "https://localhost:5050";
        public const string EVENT_HOST = "https://localhost:5051";
        public const string PROJECT_HOST = "https://localhost:5052";
        public const string GATEWAYS_HOST = "https://localhost:5053";
        public const string FRONTEND_HOST = "http://localhost:3000";


        //Add-Migration InitalCreate -OutputDir Data/Migrations -Project Auth.Data -StartupProject Auth.API
        //Add-Migration InitalCreate -OutputDir Data/Migrations -Project Event.Infrastructure -StartupProject Event.Features
        //Add-Migration InitalCreate -OutputDir Data/Migrations -Project Project.Infrastructure -StartupProject Project.API
    }
}
