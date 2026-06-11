using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }
        public async Task<PostDto> GetPostAsync(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if(post == null) throw new Exception("Post not found");

            var postDto = new PostDto()
            {
                Id = post.Id,
                Header = post.Header,
                TextContent = post.IsTextDeleted == false ? post.TextContent : "Text content removed by admin",
                CreatedAt = post.CreatedAt,
                UserId = post.UserId,
                DisplayName = post.User.DisplayName,
                AvatarUrl = post.User.AvatarUrl,
                IsTextDeleted = post.IsTextDeleted,
            };
            return postDto; 

            
        }
        public async Task<List<PostDto>> GetPostsAsync(int subSubjectId, int start, int count)
        {
            var posts = await _postRepository.GetPostsAsync(subSubjectId, start, count);
            var postsDto = posts.Select(x => new PostDto
            {
                Id = x.Id,
                Header = x.Header,
                TextContent = x.IsTextDeleted == false ? x.TextContent : "Text content removed by admin",
                CreatedAt = x.CreatedAt,
                UserId = x.UserId,
                DisplayName = x.User.DisplayName,
                AvatarUrl = x.User.AvatarUrl,
                IsTextDeleted = x.IsTextDeleted,
            }).ToList();
            return postsDto;
        }
        public async Task CreatePostAsync(CreatePostDto postDto, string userId)
        {
            var post = new SnackisPost
            {
                Header = postDto.Header,
                TextContent = postDto.TextContent,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                SnackisSubSubjectId = postDto.SubSubjectId,
                IsDeleted = false,
                IsTextDeleted = false,
            };
            await _postRepository.CreatePostAsync(post);
        }
        public async Task DeletePostAsync(DeletePostDto deletePostDto, int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) throw new Exception("Post not found");
            if(deletePostDto.RemovePost == true)
            {
                post.IsDeleted = true;
            }
            if(deletePostDto.RemoveTextContent == true)
            {
                post.IsTextDeleted = true;
            }
            await SetReportsToSolvedFromPost(postId);
            await _postRepository.SaveChangesAsync();
        }
        public async Task<List<PostCommentDto>> GetPostCommentsAsync(int postId, int start, int count)
        {
            var postComments = await _postRepository.GetPostCommentsAsync(postId, start, count);
            var postCommentsDto = postComments.Select(x => new PostCommentDto
            {
                Id = x.Id,
                TextContent = x.IsDeleted == false ? x.TextContent : "Text content removed by admin",
                CreatedAt = x.CreatedAt,
                UserId = x.UserId,
                DisplayName = x.User.DisplayName,
                AvatarUrl = x.User.AvatarUrl,
                IsDeleted = x.IsDeleted


            }).ToList();
            return postCommentsDto;
        }
        public async Task<bool> CreatePostCommentAsync(CreatePostCommentDto postCommentDto, string userId)
        {
            var postComment = new SnackisPostComment
            {
                TextContent = postCommentDto.TextContent,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                SnackisPostId = postCommentDto.SnackisPostId
            };
            var result = await _postRepository.CreatePostCommentAsync(postComment);
            return result;
        }
        public async Task DeletePostCommentAsync(int postCommentId)
        {
            var postComment = await _postRepository.GetPostCommentByIdAsync(postCommentId);
            if (postComment == null) throw new Exception("Post comment not found");
            postComment.IsDeleted = true;

            await SetReportsToSolvedFromPostComment(postCommentId);
            await _postRepository.SaveChangesAsync();
        }
        public async Task<List<ReportDto>> GetReportsAsync(int count, bool solved)
        {
            var reports = await _postRepository.GetReportsAsync(count, solved);
            var reportDtos = new List<ReportDto>();
            foreach(var report in reports)
            {
                var post = report.Post;
                var postComment = report.PostComment;

                var reportDto = new ReportDto
                {
                    ReportId = report.Id,
                    PostId = report.PostId,
                    PostCommentId = report.PostCommentId,
                    ReportedHeader = post?.Header,
                    ReportedText = post == null ? postComment.TextContent : post.TextContent,
                    ReportedUserId = post == null ? postComment.User.Id : post.User.Id,
                    ReportedUserDisplayName = post == null ? postComment.User.DisplayName : post.User.DisplayName,
                    ReportedUserAvatarUrl = post == null ? postComment.User.AvatarUrl : post.User.AvatarUrl,
                    Comment = report.Comment,
                    Solved = report.Solved,
                };
                reportDtos.Add(reportDto);
            }
            return reportDtos;

        }
        public async Task CreateReportAsync(CreateReportDto reportDto)
        {
            reportDto.PostId = reportDto.PostId == 0 ? null : reportDto.PostId;
            reportDto.PostCommentId = reportDto.PostCommentId == 0 ? null : reportDto.PostCommentId;

            if (reportDto.PostId != null && reportDto.PostCommentId != null)
            {
                throw new Exception("You can only report a specific post or post comment not both");
            }

            if(reportDto.PostId != null)
            {
                var post = await _postRepository.GetPostByIdAsync((int)reportDto.PostId);
                if (post == null) throw new Exception("Post not found");
            } else if(reportDto.PostCommentId != null)
            {
                var postComment = await _postRepository.GetPostByIdAsync((int)reportDto.PostCommentId);
                if (postComment == null) throw new Exception("Post comment not found");
            } else
            {
                throw new Exception("Need to specify post or a post comment");
            }

            var report = new SnackisReport
            {
                PostId = reportDto.PostId,
                PostCommentId = reportDto.PostCommentId,
                Comment = reportDto.Comment,
                CreatedAt = DateTime.Now,
                Solved = false
            };
            await _postRepository.CreateReportAsync(report);
        }
        public async Task UpdateReportAsync(UpdateReportDto updateReportDto)
        {
            var report = await _postRepository.GetReportByIdAsync(updateReportDto.ReportId);

            if (report == null) throw new Exception("Report not found");

            if (report.PostCommentId.HasValue)
            {
                await SetReportsToSolvedFromPostComment(report.PostCommentId.Value);
            }

            else if (report.PostId.HasValue)
            {
                await SetReportsToSolvedFromPost(report.PostId.Value);
            }
            else
            {
                throw new Exception("Invalid report (no PostId or PostCommentId)");
            }


        }
        private async Task SetReportsToSolvedFromPost(int postId)
        {
            var reports = await _postRepository.GetReportsFromPostAsync(postId);
            foreach (var report in reports)
            {
                report.Solved = true;
            }
            await _postRepository.SaveChangesAsync();
        }
        private async Task SetReportsToSolvedFromPostComment(int postCommentId)
        {
            var reports = await _postRepository.GetReportsFromPostCommentAsync(postCommentId);
            foreach (var report in reports)
            {
                report.Solved = true;
            }
            await _postRepository.SaveChangesAsync();
        }
        
    }
}
