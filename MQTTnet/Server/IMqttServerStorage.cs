﻿// Decompiled with JetBrains decompiler
// Type: MQTTnet.Server.IMqttServerStorage
// Assembly: MQTTnet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A57D64C8-A58A-4661-AABB-22ABAFCAAE1A
// Assembly location: C:\Users\ace12\Documents\xinchengbio\code\xc_client\DllMerge\dlls\MQTTnet.dll

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MQTTnet.Server
{
  public interface IMqttServerStorage
  {
    Task SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages);

    Task<IList<MqttApplicationMessage>> LoadRetainedMessagesAsync();
  }
}
