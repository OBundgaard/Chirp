using Microsoft.EntityFrameworkCore;
using TweetPostingService;
using TweetPostingService.Models;

namespace TweetServiceTesting
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestPostTweet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TweetDbContext>()
                .UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True")
                .Options;

            using var context = new TweetDbContext(options);
            var tweetService = new Program();
            typeof(Program).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.SetValue(null, context);

            var tweet = new Tweet { AuthorID = 1, Content = "Hello, World!" };

            // Act
            var result = await Program.PostTweet(tweet.AuthorID, tweet.Content);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestGetTweetsByTweetID()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TweetDbContext>()
                .UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True")
                .Options;

            using var context = new TweetDbContext(options);
            var tweetService = new Program();
            typeof(Program).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.SetValue(null, context);

            var tweet = new Tweet { AuthorID = 1, Content = "Hello, World!" };
            context.Tweets.Add(tweet);
            await context.SaveChangesAsync();

            // Act
            var result = await Program.GetTweetsByTweetID(tweet.TweetID);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(tweet.TweetID, result.First().TweetID);
            Assert.Equal(tweet.Content, result.First().Content);
        }

        [Fact]
        public async Task TestGetTweetsByAuthorID()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TweetDbContext>()
                .UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True")
                .Options;

            using var context = new TweetDbContext(options);
            var tweetService = new Program();
            typeof(Program).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.SetValue(null, context);
            var tweet = new Tweet { AuthorID = 1, Content = "Hello, World!" };
            context.Tweets.Add(tweet);
            await context.SaveChangesAsync();

            // Act
            var result = await Program.GetTweetsByAuthorID(tweet.AuthorID);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(tweet.TweetID, result.First().TweetID);
            Assert.Equal(tweet.Content, result.First().Content);


        }

        [Fact]
        public async Task TestGetAllTweets()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TweetDbContext>()
                .UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True")
                .Options;

            using var context = new TweetDbContext(options);
            var tweetService = new Program();
            typeof(Program).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.SetValue(null, context);
            var tweet = new Tweet { AuthorID = 1, Content = "Hello, World!" };
            context.Tweets.Add(tweet);
            await context.SaveChangesAsync();

            // Act
            var result = await Program.GetAllTweets();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(tweet.TweetID, result.First().TweetID);
            Assert.Equal(tweet.Content, result.First().Content);
        }

        [Fact]
        public async Task TestDeleteTweet()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TweetDbContext>()
                .UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True")
                .Options;

            using var context = new TweetDbContext(options);
            var tweetService = new Program();
            typeof(Program).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.SetValue(null, context);
            var tweet = new Tweet { AuthorID = 1, Content = "Hello, World!" };
            context.Tweets.Add(tweet);
            await context.SaveChangesAsync();

            // Act
            await Program.DeleteTweet(tweet.TweetID);

            // Assert
            var result = await Program.GetTweetsByTweetID(tweet.TweetID);
            Assert.Empty(result);
        }
    }
}