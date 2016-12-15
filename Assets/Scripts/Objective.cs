using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;

public class Objective{
	private Vector3 passengerPosition = new Vector3();
	private Vector3 dropzonePosition = new Vector3();
	private int id;
	private int money;
	private static int total = 0;

	public Objective ()
	{
		money = 0;
		this.id = total;
		total++;
	}

	public void SetPassengerPosition(Vector3 position){
		this.passengerPosition = position;
	}

	public void SetDropzonePosition(Vector3 position){
		this.dropzonePosition = position;
	}

	public Vector3 GetPassengerPosition(){
		return this.passengerPosition;
	}

	public Vector3 GetDropzonePosition(){
		return this.dropzonePosition;
	}

	public int GetId(){
		return id;
	}

	public void SetMoney(int money){
		this.money = money;
	}

	public int GetMoney(){
		return money;
	}

	public static List<Objective> LoadAll(){
		List <Objective> objectives = new List<Objective> ();
		TextAsset data = Resources.Load ("coordinates") as TextAsset;
		string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
		var lines = Regex.Split (data.text, LINE_SPLIT_RE);

		for(int i=0; i < lines.Length; i++)
		{
			Objective objective = new Objective ();
			var values = lines[i].Split(';');
			Debug.Log(values.ToString());
			float x = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
			float y = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
			objective.SetPassengerPosition(new Vector3(x, y, 0f));

			x = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);
			y = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
			objective.SetDropzonePosition (new Vector3 (x, y, 0f));

			int money =  int.Parse(values[4]);
			objective.SetMoney (money);
			objectives.Add (objective);
			Debug.Log (objective);
		}
		return objectives;
	}
}

