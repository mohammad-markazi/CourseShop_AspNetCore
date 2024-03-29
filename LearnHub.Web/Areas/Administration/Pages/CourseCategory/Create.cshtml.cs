﻿using System.ComponentModel.DataAnnotations;
using LearnHub.Application.CourseCategory.Command.Create;
using LearnHub.Application.CourseCategory.Query;
using Learnhub.Domain.ValueObjects;
using LearnHub.Web.Areas.Administration.Pages.CourseCategory.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnHub.Web.Areas.Administration.Pages.CourseCategory
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CourseCategoryInput CourseCategory { get; set; }

        public SelectList Categories { get; set; }
        public async Task OnGet()
        {
           var result= await _mediator.Send(new GetCourseCategorySelectListQuery.Request()
           {
               IsFindAllCategory = false
           });

           Categories = new SelectList(result, "Id", "Name");

        }

        public async Task<RedirectToPageResult> OnPost()
        {

            await _mediator.Send(new CreateCourseCategoryCommand.Request()
            {
                Seo = new Seo(CourseCategory.Slug, CourseCategory.Keywords, CourseCategory.MetaDescription),
            Name = CourseCategory.Name,
                ParentId = CourseCategory.ParentId
            });

           return RedirectToPage("./Index");
        }
    }

    
}
