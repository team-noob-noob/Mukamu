using System;
using System.Collections.Generic;
using System.Dynamic;
using Mukamu.Core.Interfaces.Factories;
using Mukamu.Core.Models;

namespace Sinuka.Infrastructure.Database
{
    public class ModelFactory :
        IAttachmentFactory,
        ICommentFactory,
        IConverstationFactory,
        IMessageFactory,
        IPostFactory,
        ISubCommentFactory,
        IUserFactory
    {
        public Attachment CreateAttachment(string extension, string filename, byte[] blobData)
            => new Attachment(filename, extension, blobData);

        public Comment CreateComment(string message, User commenter)
            => new Comment(message, commenter);

        public Conversation CreateConversation(ICollection<User> users)
            => new Conversation(users);

        public Message CreateMessage(string actualMessage, User sender)
            => new Message(actualMessage, sender);

        public Post CreatePost(string message, User poster)
            => new Post(poster, message);

        public SubComment CreateSubComment(string message, User commenter)
            => new SubComment(message, commenter);

        public User CreateUser(Guid externalId)
            => new User(externalId);
    }
}
