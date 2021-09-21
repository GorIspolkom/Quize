using System.Net;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Mail;
using Assets.Scripts.ProfileData;
using Quiz.QuizManagerSystem;
using Quiz.Configuration;

namespace Quiz.Models
{
    public class MailSender
    {
        /// <summary>
        /// For creating SMTP client
        /// _smtpName - name of mail (smtp.yandex.ru, smtp.gmail.com)
        /// _smtpPort - port of mail (usually 25, ���� �������� ��� ������� ��������� ���� �����)
        /// _managerMail - ���� ��������� ��������, �������� ������ ��������� ������
        /// </summary>

        string _smtpName = "smtp.mail.ru";
        int _smtpPort = 25;
        string _managerMail = "sgurev@gmail.com";

        /// <summary>
        /// _fromMail - ������� ����������, � �������� ����� ����� ���������, ����� ������ �� ����� �� smtp ������
        /// _fromPassword - ������ � ����� ����������
        /// _companyName - �������� ��������, �� ���� ����� ��������� ����
        /// </summary>

        string _fromMail = "porsche.quiz@mail.ru";
        string _fromPassword = "a6dW5SI4sb7ibAhQ6By8";
        string _companyName = "Porsche Quiz";

        // _smtp - ������ ��� �������� ��������� 

        private SmtpClient _smtp;

        // ���������� ����� 
        // questions - ������ ��������, userMail - ���� ������������ user - ��������� �����(���� ������ ���� userMail � rightAnswers)
        // rightAnswers - ���-�� ������ �������
        public void SendMail(QuizData data)
        {
            // ������ ���������� ������ ( ����� ��������� ����� �������, �� �����, �� �������� )
            _smtp = new SmtpClient(_smtpName, _smtpPort);
            _smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtp.UseDefaultCredentials = false;
            _smtp.Credentials = new NetworkCredential(_fromMail, _fromPassword);
            _smtp.EnableSsl = true;
            string mailBody = "";

            mailBody += $"<h3> ����� ��������� ��������� : {DateTime.Now} </h3>\n" +
                $"<h3> ��� ���������: {Profile.profileData.name} </h3>\n" +
                $"<h3> ��������� ������� ���������: {Profile.profileData.phone} </h3>\n" +
                $"<h3> Email ���������: {Profile.profileData.mail} </h3>\n" +
                "<br>" +
                $"<h3> ���������� ������� {data.currentQuestion - (ConfigurationData.CountOfMistakes - data.mistakeAllowed)}/10 </h3>";

            // ����� ���������� ������
            Debug.Log(mailBody);

            try
            {
                // ������� ������ ��� ������� ��������
                MailAddress fromAdress = new MailAddress(_fromMail, _companyName);
                MailAddress managerAdress = new MailAddress(_managerMail);

                //������ ��� ���������
                MailMessage messageManager = new MailMessage(fromAdress, managerAdress);
                messageManager.Subject = $"����� �������� - ��������� {data.category}";
                messageManager.Body = mailBody;
                messageManager.IsBodyHtml = true;

                // ���� ���������
                _smtp.Send(messageManager);
                _smtp.Dispose();
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception caught in _smtp.Send: {ex.ToString()}");
            }
        }
    }
}



