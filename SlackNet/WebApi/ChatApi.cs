﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SlackNet.Objects;
using SlackNet.WebApi.Responses;
using Args = System.Collections.Generic.Dictionary<string, object>;

namespace SlackNet.WebApi
{
    public class ChatApi
    {
        private readonly WebApiClient _client;
        public ChatApi(WebApiClient client) => _client = client;

        /// <summary>
        /// Deletes a message from a channel.
        /// </summary>
        /// <param name="ts">Timestamp of the message to be deleted.</param>
        /// <param name="channelId">Channel containing the message to be deleted.</param>
        /// <param name="asUser">Pass True to delete the message as the authed user. Bot users in this context are considered authed users.</param>
        /// <param name="cancellationToken"></param>
        public Task<MessageTsResponse> Delete(string ts, string channelId, bool asUser = false, CancellationToken? cancellationToken = null) =>
            _client.Get<MessageTsResponse>("chat.delete", new Args
                {
                    { "ts", ts },
                    { "channel", channelId },
                    { "asUser", asUser }
                }, cancellationToken);

        /// <summary>
        /// Sends a /me message to a channel from the calling user.
        /// </summary>
        /// <param name="channel">Channel to send message to. Can be a public channel, private group or IM channel. Can be an encoded ID, or a name.</param>
        /// <param name="text">Text of the message to send.</param>
        /// <param name="cancellationToken"></param>
        public Task<MessageTsResponse> MeMessage(string channel, string text, CancellationToken? cancellationToken = null) =>
            _client.Get<MessageTsResponse>("chat.meMessage", new Args
                {
                    { "channel", channel },
                    { "text", text }
                }, cancellationToken);

        /// <summary>
        /// Posts a message to a public channel, private channel, or direct message/IM channel.
        /// </summary>
        /// <param name="message">The message to post</param>
        /// <param name="cancellationToken"></param>
        public Task<PostMessageResponse> PostMessage(SlackMessage message, CancellationToken? cancellationToken = null) =>
            _client.Get<PostMessageResponse>("chat.postMessage", new Args
                    {
                        { "channel", message.Channel },
                        { "text", message.Text },
                        { "parse", message.Parse },
                        { "link_names", message.LinkNames },
                        { "attachments", message.Attachments },
                        { "unfurl_links", message.UnfurlLinks },
                        { "unfurl_media", message.UnfurlMedia },
                        { "username", message.Username },
                        { "as_user", message.AsUser },
                        { "icon_url", message.IconUrl },
                        { "icon_emoji", message.IconEmoji },
                        { "thread_ts", message.ThreadTs },
                        { "reply_broadcast", message.ReplyBroadcast }
                    },
                cancellationToken);

        /// <summary>
        /// Attaches Slack app unfurl behavior to a specified and relevant message. 
        /// A user token is required as this method does not support bot user tokens.
        /// </summary>
        /// <param name="channelId">Channel ID of the message.</param>
        /// <param name="ts">Timestamp of the message to add unfurl behavior to.</param>
        /// <param name="unfurls">Dictionary mapping a set of URLs from the message to their unfurl attachment.</param>
        /// <param name="userAuthRequired">Set to True to indicate the user must install your Slack app to trigger unfurls for this domain.</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// The first time this method is executed with a particular <see cref="ts"/> and <see cref="channelId"/> combination, 
        /// the valid <see cref="unfurls"/> attachments you provide will be attached to the message. 
        /// Subsequent attempts with the same <see cref="ts"/> and <see cref="channelId"/> values will modify the same attachments, rather than adding more.
        /// </remarks>
        public Task Unfurl(string channelId, string ts, IDictionary<string, Attachment> unfurls, bool userAuthRequired = false, CancellationToken? cancellationToken = null) =>
            _client.Get("chat.unfurl", new Args
                {
                    { "channel", channelId },
                    { "ts", ts },
                    { "unfurls", unfurls },
                    { "user_auth_required", userAuthRequired }
                }, cancellationToken);

        /// <summary>
        /// Updates a message in a channel.
        /// </summary>
        /// <param name="messageUpdate">Message to update.</param>
        /// <param name="cancellationToken"></param>
        public Task<MessageUpdateResponse> Update(MessageUpdate messageUpdate, CancellationToken? cancellationToken = null) =>
            _client.Get<MessageUpdateResponse>("chat.update", new Args
                    {
                        { "ts", messageUpdate.Ts },
                        { "channel", messageUpdate.ChannelId },
                        { "text", messageUpdate.Text },
                        { "attachments", messageUpdate.Attachments },
                        { "parse", messageUpdate.Parse },
                        { "link_names", messageUpdate.LinkNames },
                        { "as_user", messageUpdate.AsUser }
                    },
                cancellationToken);
    }
}