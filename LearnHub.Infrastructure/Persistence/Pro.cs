using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LearnHub.Application.Course.Query;
using Learnhub.Domain.ValueObjects;
using Dapper.FluentMap.Conventions;
using System.Reflection;

namespace LearnHub.Infrastructure.Persistence
{
    public class TypePrefixConvention : Convention
    {
        public TypePrefixConvention()
        {
            // Map all properties of type int and with the name 'id' to column 'autID'.
            Properties<string>()
                .Where(c =>  c.Name== "Slug")
                .Configure(c => c.HasColumnName("Seo_Slug"));

            // Prefix all properties of type string with 'str' when mapping to column names.
             Properties<string>()
                .Where(c =>  c.Name== "MetaDescription")
                .Configure(c => c.HasColumnName("Seo_MetaDescription"));

             Properties<string>()
                 .Where(c =>  c.Name == "Keywords")
                 .Configure(c => c.HasColumnName("Seo_Keywords"));


            // Prefix all properties of type int with 'int' when mapping to column names.
        }
    }
    public class ProductMap : EntityMap<CourseViewModel>
    {
        public static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            Expression expr = lambda;
            while (true)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;

                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;

                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expr;
                        var baseMember = memberExpression.Member;
                        Type paramType;
                        while (memberExpression != null)
                        {
                            paramType = memberExpression.Type;
                            if (paramType.GetMembers().Any(member => member.Name == baseMember.Name))
                            {
                                return paramType.GetMember(baseMember.Name)[0];
                            }

                            memberExpression = memberExpression.Expression as MemberExpression;
                        }

                        // Make sure we get the property from the derived type.
                        paramType = lambda.Parameters[0].Type;
                        return paramType.GetMember(baseMember.Name)[0];

                    default:
                        return null;
                }
            }
        }
        public static object Test(CourseViewModel model)
        {
            if (model.Seo is null)
            {
                model.Seo=new Seo();
            }
            return model.Seo.Slug;
        }


        public ProductMap()
        {
            
            Map(p=>p.Seo.Slug)
                .ToColumn("Seo_Slug");
            Map(p => p.Seo.Keywords)
                .ToColumn("Seo_Keywords");
            Map(p => p.Seo.MetaDescription)
                .ToColumn("Seo_MetaDescription");
        }
    }
}
