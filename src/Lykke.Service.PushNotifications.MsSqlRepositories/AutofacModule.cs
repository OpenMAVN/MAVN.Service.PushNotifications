﻿using System;
using Autofac;
using AutoMapper;
using Falcon.Common.Encryption;
using Lykke.Common.MsSql;
using Lykke.Service.PushNotifications.Domain.Repositories;
using Lykke.Service.PushNotifications.MsSqlRepositories.Repositories;

namespace Lykke.Service.PushNotifications.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();

            builder.RegisterMsSql(
                _connectionString,
                connString => new DatabaseContext(connString, false),
                dbConn => new DatabaseContext(dbConn));

            builder.RegisterType<PushNotificationRegistrationRepository>()
                .As<IPushNotificationRegistrationRepository>()
                .SingleInstance();

            builder.RegisterType<NotificationMessageRepository>()
                .As<INotificationMessageRepository>()
                .SingleInstance();

            var encryptionKey = Environment.GetEnvironmentVariable("EncryptionKey");
            var encryptionIv = Environment.GetEnvironmentVariable("EncryptionIV");

            var serializer = new AesSerializer(encryptionKey, encryptionIv);
            builder.RegisterInstance(serializer)
                .As<IAesSerializer>()
                .SingleInstance();

            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .SingleInstance();
        }
    }
}
