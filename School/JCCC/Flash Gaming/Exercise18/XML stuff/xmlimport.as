﻿package
{
	import flash.display.*;
	import flash.events.*;
	import flash.net.URLLoader;
	import flash.net.URLRequest;

	public class xmlimport extends MovieClip
	{
		private var xmldata:XML;

		public function xmlimport()
		{
			xmldata = new XML();
			var xmlURL:URLRequest = new URLRequest("xmltestdata.xml");
			var xmlLoader:URLLoader = new URLLoader(xmlURL);
			xmlLoader.addEventListener(Event.COMPLETE, xmlLoaded);
			xmlLoader.addEventListener(IOErrorEvent.IO_ERROR,xmlLoadError);
		}

		function xmlLoaded(event:Event) {
			xmldata  = XML(event.target.data);
			xmlFileContents.appendText("Answer 1: " + xmldata.item.answers.answer[1]);
			xmlFileContents.appendText("\nData loaded successfully.");
		}

		function xmlLoadError(event:IOErrorEvent) {
			trace(event.text);
		}
	}
}
