﻿//
// Copyright 2020 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//
using Google.Solutions.IapDesktop.Application.ObjectModel;
using Moq;
using System;

namespace Google.Solutions.Testing.Application.ObjectModel
{
    public static class ServiceRegistryExtensions
    {
        public static Mock<T> AddMock<T>(this ServiceRegistry registry)
            where T : class
        {
            var mock = new Mock<T>();
            registry.AddSingleton<T>(mock.Object);
            return mock;
        }

        public static Service<T> AsService<T>(this Mock<T> mock)
            where T : class
        {
            var provider = new Mock<IServiceProvider>();
            provider
                .Setup(p => p.GetService(It.Is<Type>(t => t == typeof(T))))
                .Returns(mock.Object);
            return new Service<T>(provider.Object);
        }
    }
}
