using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class IngameUI : BaseUI
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI timer;
	[SerializeField] private GameObject startImage;

	private void OnEnable()
	{
		EventManager.levelLoadedEvent.AddListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.AddListener(OnLevelSuccess);
		EventManager.levelFailEvent.AddListener(OnLevelFail);
		EventManager.levelStartEvent.AddListener(SetHiddenStartObjects);
	}

	private void OnDisable()
	{
		EventManager.levelLoadedEvent.RemoveListener(OnLevelLoaded);
		EventManager.levelSuccessEvent.RemoveListener(OnLevelSuccess);
		EventManager.levelFailEvent.RemoveListener(OnLevelFail);
		EventManager.levelStartEvent.RemoveListener(SetHiddenStartObjects);
	}

    private void OnLevelLoaded(LevelLoadedEventData eventData)
	{
		levelText.text = "LEVEL " + eventData.LevelNo.ToString();
		SetShown();
		SetShownStartObjects();
	}
	private void SetHiddenStartObjects()
    {
		startImage.SetActive(false);
	}
	private void SetShownStartObjects()
	{
		startImage.SetActive(true);
	}

	private void OnLevelFail()
	{
		SetHidden();
	}
	
	private void OnLevelSuccess()
	{
		SetHidden();
	}
	public void IncreaseScore(short newValue)
    {
		scoreText.text = newValue.ToString();

	}
	public void Countdown(short remainTime)
    {
		timer.text = remainTime.ToString();
	}
}
