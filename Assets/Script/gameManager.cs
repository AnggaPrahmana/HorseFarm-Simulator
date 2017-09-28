using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class item{
	public GameObject _Horse;
	public Button.ButtonClickedEvent _button;
}
public class gameManager : MonoBehaviour {
	public GameObject UI_listButton;
	public Transform List_Panel;
	public List<item> itemList;
	
	//Log activity
	List<logHistory> _logHistory;
	string irlHour, irlMinute, irlSecond, temp, textLogTemp;
	Text textLog;

	//player utility
	bool zoomIn,zoomOut,pause,dropMenus,showStatus,showLog;
	GameObject player;
	public GameObject menu;
	public static bool isPause;
	public Button food;
	public Transform drop1,drop2,drop3,PausePanel,HorseStatusPanel,LogPanel;
	float x1,x2,x3,x4,x5,x6;
	int flag;

	[SerializeField]
	GameObject[] Horseinfo;
	[SerializeField]
	RenderTexture[] _potrait;
	[SerializeField]
	RawImage potrait;
	//[SerializeField]
	//Text[] _status;
	[SerializeField]
	Text identity,power,speed;
	gameManager(){
		_logHistory = new List<logHistory> ();
	}
	void Start(){
		PopulateList();
		identity.text = Horseinfo[0].name.ToString();
		power.text = Random.Range(400,600).ToString();
		speed.text = Horseinfo[0].GetComponent<ObjectBehaviour>().moveSpeed.ToString();

	//------------------------------------------------

		zoomIn = false;
		zoomOut = false;
		pause = false;
		dropMenus = false;
		isPause = false;
		showStatus = false;
		showLog = false;
		flag = 1;

	//-------------------------------------------------
		textLog = GameObject.Find ("Log").GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update(){
		irlHour = System.DateTime.Now.Hour.ToString ();
		irlMinute = System.DateTime.Now.Minute.ToString ();
		irlSecond = System.DateTime.Now.Second.ToString ();

		textLog.text = textLogTemp;

		//-------------------------------------------------

		if(zoomIn){
			float newCamZpos = Camera.main.transform.position.z;
			float newCamYpos = Camera.main.transform.position.y;
			if(flag==1){
				newCamZpos += 0.5f;
				newCamYpos -= 0.5f;
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,newCamYpos,newCamZpos);
				if(Camera.main.transform.position.z >= -70){
					zoomIn = false;
					flag = 0;
				}
			}
		}
		else if(zoomOut){
			float newCamZpos = Camera.main.transform.position.z;
			float newCamYpos = Camera.main.transform.position.y;
			if(flag==0){
				newCamZpos -= 0.5f;
				newCamYpos += 0.5f;
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,newCamYpos,newCamZpos);
				if(Camera.main.transform.position.z <= -90){
					zoomOut = false;
					flag = 1;
				}
			}
		}

		if(dropMenus){
			float newMenu1YPos = drop1.position.y;
			float newMenu2YPos = drop2.position.y;
			float newMenu3YPos = drop3.position.y;
			newMenu1YPos -= x1;
			newMenu2YPos -= x2;
			newMenu3YPos -= x3;
			drop1.position = new Vector3(drop1.position.x,newMenu1YPos,drop1.position.z);
			drop2.position = new Vector3(drop2.position.x,newMenu2YPos,drop2.position.z);
			drop3.position = new Vector3(drop3.position.x,newMenu3YPos,drop3.position.z);
			if(drop1.localPosition.y <= -70){
				x1 = 0;
			}
			if(drop2.localPosition.y <= -118){
				x2 = 0;
			}
			if(drop3.localPosition.y <= -170){
				x3 = 0;
			}

		}
		else if(!dropMenus){
			float newMenu1YPos = drop1.transform.position.y;
			float newMenu2YPos = drop2.transform.position.y;
			float newMenu3YPos = drop3.transform.position.y;
			newMenu1YPos += x1;
			newMenu2YPos += x2;
			newMenu3YPos += x3;
			drop1.position = new Vector3(drop1.position.x,newMenu1YPos,drop1.position.z);
			drop2.position = new Vector3(drop2.position.x,newMenu2YPos,drop2.position.z);
			drop3.position = new Vector3(drop3.position.x,newMenu3YPos,drop3.position.z);
			if(drop1.localPosition.y >= -20){
				x1 = 0;
			}
			if(drop2.localPosition.y >= -20){
				x2 = 0;
			}
			if(drop3.localPosition.y >= -20){
				x3 = 0;
			}
		}

		if(pause){
			float newPanelPausePos = PausePanel.position.y;
			newPanelPausePos -= x4;
			PausePanel.position = new Vector3(PausePanel.position.x,newPanelPausePos,PausePanel.position.z);
			if(PausePanel.localPosition.y <= 0){
				x4 = 0;
				Time.timeScale = 0;
			}
		}
		else if(!pause){
			float newPanelPausePos = PausePanel.position.y;
			newPanelPausePos += x4;
			PausePanel.position = new Vector3(PausePanel.position.x,newPanelPausePos,PausePanel.position.z);
			if(PausePanel.localPosition.y >= 450){
				x4 = 0;
				Time.timeScale = 1;
			}
		}

		if(showStatus){
			potrait.gameObject.SetActive(true);
			float newPanelPos = HorseStatusPanel.position.y;
			newPanelPos += x5;
			HorseStatusPanel.position = new Vector3(HorseStatusPanel.position.x,newPanelPos,HorseStatusPanel.position.z);
			if(HorseStatusPanel.localPosition.y >= 0){
				x5 = 0;
			}
		}
		else if(!showStatus){
			float newPanelPos = HorseStatusPanel.position.y;
			newPanelPos -= x5;
			HorseStatusPanel.position = new Vector3(HorseStatusPanel.position.x,newPanelPos,HorseStatusPanel.position.z);
			if(HorseStatusPanel.localPosition.y <= -450){
				x5 = 0;
				potrait.gameObject.SetActive(false);
			}
		}

		if(showLog){
			float newPanelPos = LogPanel.position.x;
			newPanelPos += x6;
			LogPanel.position = new Vector3(newPanelPos,LogPanel.position.y,LogPanel.position.z);
			if(LogPanel.position.x >= -5){
				x6 = 0;
			}
		}
		else if(!showLog){
			float newPanelPos = LogPanel.position.x;
			newPanelPos -= x6;
			LogPanel.position = new Vector3(newPanelPos,LogPanel.position.y,LogPanel.position.z);
			if(LogPanel.position.x <= -330){
				x6 = 0;
			}
		}

		//getPlayerHasCallSupplyBox
		food.GetComponent<Button>().interactable = player.GetComponent<playerSC>().dropableFood;
	}

	public void AddToLog(string t){
		string logTime = irlHour + ":" + irlMinute + ":" + irlSecond;

		textLogTemp += logTime + " (" + t + "\n";
		_logHistory.Add (new logHistory (logTime, t));
	}

	public void saveToText(){
		string logTime = irlHour + "_" + irlMinute + "_"+ irlSecond+".txt";
		StreamWriter SW;
		string folderPath = System.IO.Path.Combine(@"D:\Projects\Program TA\Horse Farm - Simulator\Assets\LOG", logTime);
		SW = new StreamWriter(folderPath);
		foreach (logHistory lh in _logHistory) {
			temp = lh.time +" | "+lh.logContent+"\n";
			SW.WriteLine (temp);
		}

		SW.Close ();
		Debug.Log ("Save to file...");
	}

	public void zoom(int z){
		if(z==1){
			zoomIn = true;
			zoomOut = false;
		}else if(z==2){
			zoomOut = true;
			zoomIn = false;
		}
	}

	public void DropDown(){
		dropMenus = !dropMenus;
		x1 = 3f;
		x2 = 3f;
		x3 = 3f;
	}

	public void PauseMenu(){
		pause = !pause;
		isPause = !isPause; // en/disable rotating planet
		x4 = 20f;
	}

	public void showHorsePanel(){
		showStatus = !showStatus;
		isPause = !isPause;
		x5 = 20f;
	}

	public void showLogPanel(){
		showLog = !showLog;
		x6 = 5f;
	}

	public void buyFood(){
		player.GetComponent<playerSC>().isBuy = true;
	}

	void PopulateList(){
		foreach(var item in itemList){
			GameObject newListButton = Instantiate(UI_listButton) as GameObject;
			UI_ListScript _uiListScript = newListButton.GetComponent<UI_ListScript>();
			_uiListScript.Property.text = item._Horse.name.ToString();
			_uiListScript.transform.SetParent(List_Panel);
			_uiListScript.button.onClick = item._button;
		}
	}

	public void giveInfo(int i){
		potrait.texture = _potrait[i];
		identity.text = Horseinfo[i].name.ToString();
		power.text = Random.Range(400,600).ToString();
		speed.text = Horseinfo[i].GetComponent<ObjectBehaviour>().moveSpeed.ToString();
		//if(_status[i])
	}
}
