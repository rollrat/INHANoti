// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using NotiServer.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotiServer.Postprocessor
{
    /// <summary>
    /// Postprocessor interface
    /// </summary>
    public abstract class IPostprocessor
    {
        public abstract void Run(NetTask task);
    }
}
