﻿// Decompiled with JetBrains decompiler
// Type: MQTTnet.Exceptions.MqttConfigurationException
// Assembly: MQTTnet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A57D64C8-A58A-4661-AABB-22ABAFCAAE1A
// Assembly location: C:\Users\ace12\Documents\xinchengbio\code\xc_client\DllMerge\dlls\MQTTnet.dll

using System;

namespace MQTTnet.Exceptions
{
  public class MqttConfigurationException : Exception
  {
    protected MqttConfigurationException()
    {
    }

    public MqttConfigurationException(Exception innerException)
      : base(innerException.Message, innerException)
    {
    }

    public MqttConfigurationException(string message)
      : base(message)
    {
    }
  }
}
