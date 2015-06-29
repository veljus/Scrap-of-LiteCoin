private void timer1_Tick(object sender, EventArgs e)
{
    string str_url = "https://btc-e.com/exchange/ltc_eur";
    HtmlWeb web = new HtmlWeb();
    HtmlAgilityPack.HtmlDocument doc = web.Load(str_url);
    HtmlAgilityPack.HtmlNode justOneNode = doc.DocumentNode.SelectSingleNode("//span[@id='last27']");
    string str_write;
    str_write = justOneNode.InnerHtml.ToString();

    justOneNode = doc.DocumentNode.SelectSingleNode("//span[@id='last19']");
    string str_write2 = justOneNode.InnerHtml.ToString();
    string str_write3;
   
    str_write3 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    string str_my_json1;

    str_my_json1 = "{\"ID\":\"" + str_write3 + "\",\"LTC\":\"" + str_write + "\",\"BTC\":\"" + str_write2 + "\"}";

    lblLtc.Text = str_write + str_write3;
    lblBTC.Text = str_my_json1;

    var httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create("http://www.cirvirlab.com/velja.php?p=veljus");
    httpWebRequest.ContentType = "text/json";
    httpWebRequest.Method = "POST";
    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    {
        streamWriter.Write(str_my_json1);
        streamWriter.Flush();
        streamWriter.Close();
    }
    var httpResponse = (System.Net.WebResponse)httpWebRequest.GetResponse();
    string str_response;
    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    {
        var result = streamReader.ReadToEnd();
        str_response = result.ToString();
    }
    label3.Text = str_response;
}
