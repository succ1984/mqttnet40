﻿// Decompiled with JetBrains decompiler
// Type: MQTTnet.Formatter.V5.MqttV500PropertiesWriter
// Assembly: MQTTnet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A57D64C8-A58A-4661-AABB-22ABAFCAAE1A
// Assembly location: C:\Users\ace12\Documents\xinchengbio\code\xc_client\DllMerge\dlls\MQTTnet.dll

using System;
using System.Collections.Generic;
using MQTTnet.Packets;
using MQTTnet.Protocol;

namespace MQTTnet.Formatter.V5
{
  public class MqttV500PropertiesWriter
  {
    private readonly MqttPacketWriter _packetWriter = new MqttPacketWriter();

    public int Length => _packetWriter.Length;

    public void WriteUserProperties(List<MqttUserProperty> userProperties)
    {
      if (userProperties == null || userProperties.Count == 0)
        return;
      foreach (var userProperty in userProperties)
      {
        _packetWriter.Write(38);
        _packetWriter.WriteWithLengthPrefix(userProperty.Name);
        _packetWriter.WriteWithLengthPrefix(userProperty.Value);
      }
    }

    public void WriteCorrelationData(byte[] value) => Write(MqttPropertyId.CorrelationData, value);

    public void WriteAuthenticationData(byte[] value) => Write(MqttPropertyId.AuthenticationData, value);

    public void WriteReasonString(string value) => Write(MqttPropertyId.ReasonString, value);

    public void WriteResponseTopic(string value) => Write(MqttPropertyId.ResponseTopic, value);

    public void WriteContentType(string value) => Write(MqttPropertyId.ContentType, value);

    public void WriteServerReference(string value) => Write(MqttPropertyId.ServerReference, value);

    public void WriteAuthenticationMethod(string value) => Write(MqttPropertyId.AuthenticationMethod, value);

    public void WriteTo(IMqttPacketWriter packetWriter)
    {
      if (packetWriter == null)
        throw new ArgumentNullException(nameof (packetWriter));
      packetWriter.WriteVariableLengthInteger((uint) _packetWriter.Length);
      packetWriter.Write(_packetWriter);
    }

    public void WriteSessionExpiryInterval(uint? value) => WriteAsFourByteInteger(MqttPropertyId.SessionExpiryInterval, value);

    public void WriteSubscriptionIdentifier(uint? value) => WriteAsVariableLengthInteger(MqttPropertyId.SubscriptionIdentifier, value);

    public void WriteSubscriptionIdentifiers(IEnumerable<uint> value)
    {
      if (value == null)
        return;
      foreach (var num in value)
        WriteAsVariableLengthInteger(MqttPropertyId.SubscriptionIdentifier, num);
    }

    public void WriteTopicAlias(ushort? value) => Write(MqttPropertyId.TopicAlias, value);

    public void WriteMessageExpiryInterval(uint? value) => WriteAsFourByteInteger(MqttPropertyId.MessageExpiryInterval, value);

    public void WritePayloadFormatIndicator(MqttPayloadFormatIndicator? value)
    {
      if (!value.HasValue)
        return;
      Write(MqttPropertyId.PayloadFormatIndicator, (byte) value.Value);
    }

    public void WriteWillDelayInterval(uint? value) => WriteAsFourByteInteger(MqttPropertyId.WillDelayInterval, value);

    public void WriteRequestProblemInformation(bool? value) => Write(MqttPropertyId.RequestProblemInformation, value);

    public void WriteRequestResponseInformation(bool? value) => Write(MqttPropertyId.RequestResponseInformation, value);

    public void WriteReceiveMaximum(ushort? value) => Write(MqttPropertyId.ReceiveMaximum, value);

    public void WriteMaximumPacketSize(uint? value) => WriteAsFourByteInteger(MqttPropertyId.MaximumPacketSize, value);

    public void WriteRetainAvailable(bool? value) => Write(MqttPropertyId.RetainAvailable, value);

    public void WriteAssignedClientIdentifier(string value) => Write(MqttPropertyId.AssignedClientIdentifier, value);

    public void WriteTopicAliasMaximum(ushort? value) => Write(MqttPropertyId.TopicAliasMaximum, value);

    public void WriteWildcardSubscriptionAvailable(bool? value) => Write(MqttPropertyId.WildcardSubscriptionAvailable, value);

    public void WriteSubscriptionIdentifiersAvailable(bool? value) => Write(MqttPropertyId.SubscriptionIdentifiersAvailable, value);

    public void WriteSharedSubscriptionAvailable(bool? value) => Write(MqttPropertyId.SharedSubscriptionAvailable, value);

    public void WriteServerKeepAlive(ushort? value) => Write(MqttPropertyId.ServerKeepAlive, value);

    public void WriteResponseInformation(string value) => Write(MqttPropertyId.ResponseInformation, value);

    private void Write(MqttPropertyId id, bool? value)
    {
      if (!value.HasValue)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.Write(value.Value ? (byte) 1 : (byte) 0);
    }

    private void Write(MqttPropertyId id, byte? value)
    {
      if (!value.HasValue)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.Write(value.Value);
    }

    private void Write(MqttPropertyId id, ushort? value)
    {
      if (!value.HasValue)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.Write(value.Value);
    }

    private void WriteAsVariableLengthInteger(MqttPropertyId id, uint? value)
    {
      if (!value.HasValue)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.WriteVariableLengthInteger(value.Value);
    }

    private void WriteAsFourByteInteger(MqttPropertyId id, uint? value)
    {
      if (!value.HasValue)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.Write((byte) (value.Value >> 24));
      _packetWriter.Write((byte) (value.Value >> 16));
      _packetWriter.Write((byte) (value.Value >> 8));
      _packetWriter.Write((byte) value.Value);
    }

    private void Write(MqttPropertyId id, string value)
    {
      if (value == null)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.WriteWithLengthPrefix(value);
    }

    private void Write(MqttPropertyId id, byte[] value)
    {
      if (value == null)
        return;
      _packetWriter.Write((byte) id);
      _packetWriter.WriteWithLengthPrefix(value);
    }
  }
}
