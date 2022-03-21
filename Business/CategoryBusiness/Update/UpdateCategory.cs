﻿using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CategoryRequests;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Update
{
    public class UpdateCategory : ServiceManagerBase, IRequestHandler<UpdateCategoryRequest, CategoryResponse>
    {
        private List<ValidateError> errors;
        private readonly IValidator<CategoryRequest> _validator;

        public UpdateCategory(IUnityOfWork uow, IValidator<CategoryRequest> validator) : base(uow)
        {
            _validator = validator;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeCategoryRequest(request.Map<CategoryRequest>());
                if (errors != null)
                {
                    throw new Exception();
                }
                if(_uow.Category.Where(c => c.Name == request.Name).FirstOrDefault() != null)
                {
                    throw new Exception("Category name already in use");
                }

                var obj = request.Map<Category>();

                _uow.Category.Update(obj);
                await _uow.Commit(cancellationToken);

                return obj.Map<CategoryResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeCategoryRequest(CategoryRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }
    }
}
