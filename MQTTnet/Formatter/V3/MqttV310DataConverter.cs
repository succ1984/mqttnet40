﻿// Decompiled with JetBrains decompiler
// Type: MQTTnet.Formatter.V3.MqttV310DataConverter
// Assembly: MQTTnet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A57D64C8-A58A-4661-AABB-22ABAFCAAE1A
// Assembly location: C:\Users\ace12\Documents\xinchengbio\code\xc_client\DllMerge\dlls\MQTTnet.dll

using System;
using System.Linq;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Unsubscribing;
using MQTTnet.Exceptions;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using MQTTnet.Server;
using MqttClientSubscribeResult = MQTTnet.Client.Subscribing.MqttClientSubscribeResult;

namespace MQTTnet.Formatter.V3
{
  public class MqttV310DataConverter : IMqttDataConverter
  {
    public MqttPublishPacket CreatePublishPacket(
      MqttApplicationMessage applicationMessage)
    {
      if (applicationMessage == null)
        throw new ArgumentNullException(nameof (applicationMessage));
      return new MqttPublishPacket
      {
        Topic = applicationMessage.Topic,
        Payload = applicationMessage.Payload,
        QualityOfServiceLevel = applicationMessage.QualityOfServiceLevel,
        Retain = applicationMessage.Retain,
        Dup = false
      };
    }

    public MqttPubAckPacket CreatePubAckPacket(MqttPublishPacket publishPacket)
    {
      var mqttPubAckPacket = new MqttPubAckPacket();
      mqttPubAckPacket.PacketIdentifier = publishPacket.PacketIdentifier;
      mqttPubAckPacket.ReasonCode = MqttPubAckReasonCode.Success;
      return mqttPubAckPacket;
    }

    public MqttApplicationMessage CreateApplicationMessage(
      MqttPublishPacket publishPacket)
    {
      if (publishPacket == null)
        throw new ArgumentNullException(nameof (publishPacket));
      return new MqttApplicationMessage
      {
        Topic = publishPacket.Topic,
        Payload = publishPacket.Payload,
        QualityOfServiceLevel = publishPacket.QualityOfServiceLevel,
        Retain = publishPacket.Retain
      };
    }

    public MqttClientAuthenticateResult CreateClientConnectResult(
      MqttConnAckPacket connAckPacket)
    {
      if (connAckPacket == null)
        throw new ArgumentNullException(nameof (connAckPacket));
      MqttClientConnectResultCode connectResultCode;
      switch (connAckPacket.ReturnCode.Value)
      {
        case MqttConnectReturnCode.ConnectionAccepted:
          connectResultCode = MqttClientConnectResultCode.Success;
          break;
        case MqttConnectReturnCode.ConnectionRefusedUnacceptableProtocolVersion:
          connectResultCode = MqttClientConnectResultCode.UnsupportedProtocolVersion;
          break;
        case MqttConnectReturnCode.ConnectionRefusedIdentifierRejected:
          connectResultCode = MqttClientConnectResultCode.ClientIdentifierNotValid;
          break;
        case MqttConnectReturnCode.ConnectionRefusedServerUnavailable:
          connectResultCode = MqttClientConnectResultCode.ServerUnavailable;
          break;
        case MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword:
          connectResultCode = MqttClientConnectResultCode.BadUserNameOrPassword;
          break;
        case MqttConnectReturnCode.ConnectionRefusedNotAuthorized:
          connectResultCode = MqttClientConnectResultCode.NotAuthorized;
          break;
        default:
          throw new MqttProtocolViolationException("Received unexpected return code.");
      }
      return new MqttClientAuthenticateResult
      {
        IsSessionPresent = connAckPacket.IsSessionPresent,
        ResultCode = connectResultCode
      };
    }

    public MqttConnectPacket CreateConnectPacket(
      MqttApplicationMessage willApplicationMessage,
      IMqttClientOptions options)
    {
      if (options == null)
        throw new ArgumentNullException(nameof (options));
      return new MqttConnectPacket
      {
        ClientId = options.ClientId,
        Username = options.Credentials?.Username,
        Password = options.Credentials?.Password,
        CleanSession = options.CleanSession,
        KeepAlivePeriod = (ushort) options.KeepAlivePeriod.TotalSeconds,
        WillMessage = willApplicationMessage
      };
    }

    public MqttConnAckPacket CreateConnAckPacket(
      MqttConnectionValidatorContext connectionValidatorContext)
    {
      return connectionValidatorContext != null ? new MqttConnAckPacket
      {
        ReturnCode = new MqttConnectReasonCodeConverter().ToConnectReturnCode(connectionValidatorContext.ReasonCode)
      } : throw new ArgumentNullException(nameof (connectionValidatorContext));
    }

    public MqttClientSubscribeResult CreateClientSubscribeResult(
      MqttSubscribePacket subscribePacket,
      MqttSubAckPacket subAckPacket)
    {
      if (subscribePacket == null)
        throw new ArgumentNullException(nameof (subscribePacket));
      if (subAckPacket == null)
        throw new ArgumentNullException(nameof (subAckPacket));
      if (subAckPacket.ReturnCodes.Count != subscribePacket.TopicFilters.Count)
        throw new MqttProtocolViolationException("The return codes are not matching the topic filters [MQTT-3.9.3-1].");
      var clientSubscribeResult = new MqttClientSubscribeResult();
      clientSubscribeResult.Items.AddRange(subscribePacket.TopicFilters.Select((t, i) => new MqttClientSubscribeResultItem(t, (MqttClientSubscribeResultCode) subAckPacket.ReturnCodes[i])));
      return clientSubscribeResult;
    }

    public MqttClientUnsubscribeResult CreateClientUnsubscribeResult(
      MqttUnsubscribePacket unsubscribePacket,
      MqttUnsubAckPacket unsubAckPacket)
    {
      if (unsubscribePacket == null)
        throw new ArgumentNullException(nameof (unsubscribePacket));
      if (unsubAckPacket == null)
        throw new ArgumentNullException(nameof (unsubAckPacket));
      var unsubscribeResult = new MqttClientUnsubscribeResult();
      unsubscribeResult.Items.AddRange(unsubscribePacket.TopicFilters.Select((t, i) => new MqttClientUnsubscribeResultItem(t, MqttClientUnsubscribeResultCode.Success)));
      return unsubscribeResult;
    }

    public MqttSubscribePacket CreateSubscribePacket(
      MqttClientSubscribeOptions options)
    {
      if (options == null)
        throw new ArgumentNullException(nameof (options));
      var mqttSubscribePacket = new MqttSubscribePacket();
      mqttSubscribePacket.TopicFilters.AddRange(options.TopicFilters);
      return mqttSubscribePacket;
    }

    public MqttUnsubscribePacket CreateUnsubscribePacket(
      MqttClientUnsubscribeOptions options)
    {
      if (options == null)
        throw new ArgumentNullException(nameof (options));
      var unsubscribePacket = new MqttUnsubscribePacket();
      unsubscribePacket.TopicFilters.AddRange(options.TopicFilters);
      return unsubscribePacket;
    }

    public MqttDisconnectPacket CreateDisconnectPacket(
      MqttClientDisconnectOptions options)
    {
      if (options.ReasonCode != MqttClientDisconnectReason.NormalDisconnection || options.ReasonString != null)
        throw new MqttProtocolViolationException("Reason codes and reason string for disconnect are only supported for MQTTv5.");
      return new MqttDisconnectPacket();
    }

    public MqttClientPublishResult CreatePublishResult(
      MqttPubAckPacket pubAckPacket)
    {
      return new MqttClientPublishResult
      {
        PacketIdentifier = pubAckPacket?.PacketIdentifier,
        ReasonCode = MqttClientPublishReasonCode.Success
      };
    }

    public MqttClientPublishResult CreatePublishResult(
      MqttPubRecPacket pubRecPacket,
      MqttPubCompPacket pubCompPacket)
    {
      if (pubRecPacket == null || pubCompPacket == null)
        return new MqttClientPublishResult
        {
          ReasonCode = MqttClientPublishReasonCode.UnspecifiedError
        };
      return new MqttClientPublishResult
      {
        PacketIdentifier = pubCompPacket.PacketIdentifier,
        ReasonCode = MqttClientPublishReasonCode.Success
      };
    }
  }
}
