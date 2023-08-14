using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Services.Dtos.User;
using XBInsaat.Services.Services.Interfaces;
using XBInsaat.Services.Services.Interfaces.User;
using static System.Net.Mime.MediaTypeNames;

namespace XBInsaat.Services.Services.Implementations.User
{
    public class CareerServices : ICareerServices
    {
        private readonly IEmailServices _emailServices;

        public CareerServices(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }
        public void CheckValue(CareerPostDto careerPostDto)
        {
            if (careerPostDto == null)
            {
                throw new ItemNullException("Məlumatlarınızı qeyd edin!");
            }
            if (careerPostDto.CV == null)
            {
                throw new ItemNullException("CV-nizi əlavə edin!");

            }
            if (!(careerPostDto.PhoneNumber.StartsWith("050") || careerPostDto.PhoneNumber.StartsWith("099") || careerPostDto.PhoneNumber.StartsWith("051") || careerPostDto.PhoneNumber.StartsWith("055") || careerPostDto.PhoneNumber.StartsWith("070") || careerPostDto.PhoneNumber.StartsWith("077") || careerPostDto.PhoneNumber.StartsWith("010")))
                throw new ItemFormatException("Nömrənin prefiksi yanlışdır!");
            if (careerPostDto.CV.ContentType == "application/pdf" || careerPostDto.CV.ContentType == "application/msword" || careerPostDto.CV.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                if (careerPostDto.Fullname == null)
                {
                    throw new ItemNullException("Adınızı qeyd edin");
                }
                if (careerPostDto.PhoneNumber == null)
                {
                    throw new ItemNullException("Əlaqə nömrənizi qeyd edin");
                }
                PhoneNumberPrefixValidation(careerPostDto.PhoneNumber);
               
            }
            else
            {
                throw new ItemFormatException("CV-nizi sadəcə Pdf və Word formatında əlavə edə bilərsiniz!");
            }
           
        }

        public async Task SendCV(CareerPostDto careerPostDto)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/career.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{phonenumber}}", careerPostDto.PhoneNumber);
            body = body.Replace("{{fullname}}", careerPostDto.Fullname);
            if (careerPostDto.Message == null)
                body = body.Replace("{{message}}", "Müraciət qeyd olunmayıb.");
            else
                body = body.Replace("{{message}}", careerPostDto.Message);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;


            // PDF dosyasını ek olarak ekliyoruz
            using (var memoryStream = new MemoryStream())
            {
                await careerPostDto.CV.CopyToAsync(memoryStream);
                bodyBuilder.Attachments.Add(careerPostDto.CV.FileName, memoryStream.ToArray(), ContentType.Parse(careerPostDto.CV.ContentType));
            }


            await _emailServices.Send("elnur204@gmail.com", "XBInsaat MMC Career", bodyBuilder);
        }
        private void PhoneNumberPrefixValidation(string phoneNumber)
        {
            string phoneRegex = @"^(050|051|055|070|077|010|099)(\d{7})$";
            if (phoneNumber != null)
            {
                if (!Regex.IsMatch(phoneNumber, phoneRegex))
                    throw new ItemFormatException("Nömrə yanlışdır!");
            }
        }
    }
}
