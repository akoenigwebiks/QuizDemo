using System.Xml;
using System.Xml.Linq;

namespace QuizDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            XmlDocument xmlDoc = ReadDoc("quiz-data.xml");
            int option = GetUserMenuChoice();

            switch (option)
            {
                case 1:
                    AddQuestionsCli(xmlDoc);
                    break;
                case 2:
                    AnswerQuestionsCli(xmlDoc);
                    break;
            }

            //Action operation = option switch
            //{
            //    1 => AddQuestionsCli,
            //    2 => AnswerQuestionsCli,
            //};

            //operation(xmlDoc);
            //var xmlEl2 = buildNodeWithXmlElment(qa[0], qa[1]);
            //var wtf = 0;
        }

        /// <summary>
        /// get the xml from file and return a xmldocument
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static XmlDocument ReadDoc(string path)
        {
            // ? read from file
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }

        static int GetUserMenuChoice()
        {
            // show user options
            var message = "1. Create questions \n2. Answer Questions";
            Console.WriteLine(message);
            return int.Parse(Console.ReadLine()!);
        }

        static void AddQuestionsCli(XmlDocument xmlDoc)
        {
            var writeQ = "Write a question";
            Console.WriteLine(writeQ);
            var q = Console.ReadLine();

            var writeA = $"Write the answer for {q}";
            var ans = Console.ReadLine();
            Console.WriteLine($"question:{q}\nanswer:{ans} ");
            SaveQAToFile(q, ans, xmlDoc);

        }

        static private void SaveQAToFile(string q, string a, XmlDocument xmlDoc)
        {
            XElement xmlEl1 = BuildAsXElement(q, a);
            XmlNode xN = XElementToXmlNode(xmlEl1);

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xN, true));
            xmlDoc.Save("quiz-data.xml");
        }

        static void AnswerQuestionsCli(XmlDocument xmlDoc)
        {
            // get all Items from the xml
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("Item");

            string questionsToDisplay = "";

            int i = 1;

            foreach (XmlNode node in nodes)
            {
                // get the Question and Answer from the item
                var q = node.SelectSingleNode("Question").InnerText;
                questionsToDisplay += $"{i}. {q}\n";
                i++;
            }
            Console.WriteLine(questionsToDisplay);

            Console.WriteLine("Pick a number to answer");
            var answerNum = Console.ReadLine();
            Console.WriteLine(answerNum);

            // get the answer from the user
            Console.WriteLine("Write your answer");
            var userAnswer = Console.ReadLine();

            // get the answer from the xml
            var correctAnswer = nodes[int.Parse(answerNum) - 1].SelectSingleNode("Answer").InnerText;

            // compare the answer
            var msg = userAnswer == correctAnswer ? "Correct" : "Incorrect";
            Console.WriteLine(msg);
        }

        public void SaveItem(string q, string a, XmlDocument xmlDoc)
        {
            // create a node item
            // create question item
            // create answer item


        }

        public static XElement BuildAsXElement(string q, string a)
        {
            var qNode = new XElement("Question", q);
            var aNode = new XElement("Answer", a);
            return new XElement("Item", qNode, aNode);
        }

        public static XmlNode XElementToXmlNode(XElement xElement)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xElement.ToString());
            return xDoc.FirstChild;
        }

        //public static XElement buildNodeWithXmlElment(string q, string a)
        //{
        //    //Node is an XmlNode pulled from an XmlDocument
        //    XmlDocument temp = new XmlDocument();
        //    var xmlAsString = "<Item><Question>"
        //        + q + "</Question><Answer>"
        //        +a+"</Answer></Item>";
        //    temp.LoadXml(xmlAsString);

        //    return XElement.Parse(temp.ToString()!);
        //}
    }
}
