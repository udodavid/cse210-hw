using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> comments = new List<Comment>();

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Comment
{
    public string Commenter { get; set; }
    public string Text { get; set; }

    public Comment(string commenter, string text)
    {
        Commenter = commenter;
        Text = text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video { Title = "Introduction to C#", Author = "Alice", Length = 300 };
        video1.AddComment(new Comment("John", "Great video!"));
        video1.AddComment(new Comment("Jane", "Very informative."));
        video1.AddComment(new Comment("Bob", "Helped me a lot!"));
        videos.Add(video1);

        Video video2 = new Video { Title = "Advanced C# Techniques", Author = "Bob", Length = 500 };
        video2.AddComment(new Comment("Anna", "Awesome tips!"));
        video2.AddComment(new Comment("Rick", "Loved the examples."));
        video2.AddComment(new Comment("Sara", "This is just what I needed."));
        videos.Add(video2);

        Video video3 = new Video { Title = "C# Design Patterns", Author = "Charlie", Length = 400 };
        video3.AddComment(new Comment("Tom", "Excellent content."));
        video3.AddComment(new Comment("Jerry", "Very clear explanation."));
        videos.Add(video3);

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}, Author: {video.Author}, Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Commenter}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}
