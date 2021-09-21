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
        /// _smtpPort - port of mail (usually 25, надо поискать дл€ каждого оператора свои порты)
        /// _managerMail - мыло менеджера компании, которому должно приходить письмо
        /// </summary>

        string _smtpName = "smtp.mail.ru";
        int _smtpPort = 25;
        string _managerMail = "sgurev@gmail.com";

        /// <summary>
        /// _fromMail - аккаунт приложени€, с которого будут слать сообщени€, нужно регать на такой же smtp службе
        /// _fromPassword - пароль к почте приложени€
        /// _companyName - название компании, от кого будет приходить мыло
        /// </summary>

        string _fromMail = "porsche.quiz@mail.ru";
        string _fromPassword = "a6dW5SI4sb7ibAhQ6By8";
        string _companyName = "Porsche Quiz";

        // _smtp - клиент дл€ отправки сообщений 

        private SmtpClient _smtp;

        // магический метод 
        // questions - массив вопросов, userMail - мыло пользовател€ user - структура юзера(надо внести туда userMail и rightAnswers)
        // rightAnswers - кол-во верных ответов
        public void SendMail(QuizData data)
        {
            // Ќачало заполнени€ письма ( можно сверстать через таблицы, не успел, не критично )
            _smtp = new SmtpClient(_smtpName, _smtpPort);
            _smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtp.UseDefaultCredentials = false;
            _smtp.Credentials = new NetworkCredential(_fromMail, _fromPassword);
            _smtp.EnableSsl = true;
            string mailBody = "";

            mailBody += $"<h3> ¬рем€ окончани€ викторины : {DateTime.Now} </h3>\n" +
                $"<h3> »м€ участника: {Profile.profileData.name} </h3>\n" +
                $"<h3> ћобильный телефон участника: {Profile.profileData.phone} </h3>\n" +
                $"<h3> Email участника: {Profile.profileData.mail} </h3>\n" +
                "<br>" +
                $"<h3> ѕравильных ответов {data.currentQuestion - (ConfigurationData.CountOfMistakes - data.mistakeAllowed)}/10 </h3>";

            //  онец заполнени€ письма
            Debug.Log(mailBody);

            try
            {
                // —оздаем адреса дл€ посылки собщений
                MailAddress fromAdress = new MailAddress(_fromMail, _companyName);
                MailAddress managerAdress = new MailAddress(_managerMail);

                //письмо дл€ менеджера
                MailMessage messageManager = new MailMessage(fromAdress, managerAdress);
                messageManager.Subject = $"Ќовый участник - ¬икторина {data.category}";
                messageManager.Body = mailBody;
                messageManager.IsBodyHtml = true;

                // шлем сообщени€
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



