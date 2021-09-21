using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Configuration
{
    class ConfigurationData
    {
        //количество допускаемых ошибок для победы
        public const int CountOfMistakes = 1;
        //время на ответ
        public const float TimeForAnswer = 20;
        //время выхода после поражения/победы
        public const float TimeForLeave = 15;
        //вопросов в викторине
        public const int QuestionInQuiz  = 10;
        //время между выводом элементов вопроса (вопрос, картинка, варианты ответа)
        public const float TimeBetweenElementOutput = 0.4f;
    }
}
