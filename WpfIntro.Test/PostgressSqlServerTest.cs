﻿using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using WpfIntro.DataAccessLayer.Common;
using WpfIntro.DataAccessLayer.PostgressSqlServer;

namespace WpfIntro.Test
{
    public class PostgressSqlServerTest
    {
        [Test]
        public void TestDeclareParameter()
        {
            // Arrange
            IDatabase db = new Database("connstr");
            string parameterName = "@Id";
            int parameterValue = 123;

            // Act
            DbCommand cmd = db.CreateCommand($"SELECT * FROM public.\"MediaItems\" WHERE \"Id\" ={parameterName}");
            db.DeclareParameter(cmd, parameterName, DbType.Int32);

            // Assert
            Assert.IsTrue(cmd.Parameters.Contains(parameterName));
            Assert.AreEqual(DbType.Int32, cmd.Parameters[parameterName].DbType);
            Assert.IsNull(cmd.Parameters[parameterName].Value);
        }

        [Test]
        public void Test_DeclareParameter_Duplicate_Name()
        {
            // Arrange
            IDatabase db = new Database("connstr");
            string parameterName = "@Id";
            int parameterValue = 123;

            // Act
            DbCommand cmd = db.CreateCommand($"SELECT * FROM public.\"MediaItems\" WHERE \"Id\" ={parameterName}");
            db.DeclareParameter(cmd, parameterName, DbType.Int32);
            // Assert
            var ex = Assert.Throws<ArgumentException>(() => db.DeclareParameter(cmd, parameterName, DbType.Int32));
            Assert.That(ex.Message, Is.EqualTo($"Parameter {parameterName} already exists."));
        }

        [Test]
        public void TestSetParameter()
        {
            // Arrange
            IDatabase db = new Database("connstr");
            string parameterName = "@Id";
            int parameterValue = 123;

            // Act
            DbCommand cmd = db.CreateCommand($"SELECT * FROM public.\"MediaItems\" WHERE \"Id\" ={parameterName}");
            db.DeclareParameter(cmd, parameterName, DbType.Int32);

            // Assert
            Assert.IsTrue(cmd.Parameters.Contains(parameterName));
            Assert.AreEqual(DbType.Int32, cmd.Parameters[parameterName].DbType);
            Assert.IsNull(cmd.Parameters[parameterName].Value);

            db.SetParameter(cmd, parameterName, parameterValue);
            Assert.AreEqual(parameterValue, cmd.Parameters[parameterName].Value);
        }

        [Test]
        public void Test_SetParameter_Without_Declaring_Parameter()
        {
            // Arrange
            IDatabase db = new Database("connstr");
            string parameterName = "@Id";
            int parameterValue = 123;

            // Act
            DbCommand cmd = db.CreateCommand($"SELECT * FROM public.\"MediaItems\" WHERE \"Id\" ={parameterName}");

            var ex = Assert.Throws<ArgumentException>(() => db.SetParameter(cmd, parameterName, parameterValue));
            Assert.That(ex.Message, Is.EqualTo($"Parameter {parameterName} does not exists."));
        }
    }
}