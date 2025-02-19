﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design.Serialization.Tests;

public class CodeDomSerializerTests
{
    [Fact]
    public void CodeDomSerializer_Constructor()
    {
        CodeDomSerializer underTest = new();
        Assert.NotNull(underTest);
    }
}
