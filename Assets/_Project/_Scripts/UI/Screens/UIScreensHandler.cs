using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreensHandler : MonoBehaviour
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private LossScreen _lossScreen;
    [SerializeField] private FirstClickHandler _firstClickHandler;
    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private AddCoinsEffects _addCoinsEffects;
    [SerializeField] private WalletDisplay _walletDisplay;
    [SerializeField] private HolderZone _holderZone;
    [SerializeField] private AudioSource _audioSource;

    public UnityAction UpdatingUIForNewStart;
    public UnityAction LossScreenClosed;
    public UnityAction VictoryScreenClosed;

    private void OnEnable()
    {
        _firstClickHandler.FirstClick += OnFirstClickStartScreen;
        _picturePlace.PictureComplete += OnPictureComplete;
        _picturePlace.PictureFail += OnPictureFail;
        _picturePlace.ShowedEmptyPicturePart += OnShowedEmptyPicturePart;
        _addCoinsEffects.AddCoinsAnimationEnded += EndenAddCoinsEffects;
        _lossScreen.LossScreenHide += OnPlayerLose;
    }


    private void OnDisable()
    {
        _firstClickHandler.FirstClick -= OnFirstClickStartScreen;
        _picturePlace.PictureComplete -= OnPictureComplete;
        _picturePlace.PictureFail -= OnPictureFail;
        _picturePlace.ShowedEmptyPicturePart -= OnShowedEmptyPicturePart;
        _addCoinsEffects.AddCoinsAnimationEnded -= EndenAddCoinsEffects;
        _lossScreen.LossScreenHide -= OnPlayerLose;
    }

    private void Start()
    {
        _startScreen.Show();
        _victoryScreen.Hide();
        _lossScreen.Hide();
    }

    private void OnPlayerLose()
    {
        _startScreen.Show();
        _holderZone.Activated();
        _lossScreen.Hide();
        _gameScreen.Hide();
        _firstClickHandler?.ResetClick();
        LossScreenClosed?.Invoke();
        UpdatingUIForNewStart?.Invoke();
    }

    private void OnFirstClickStartScreen()
    {
        _startScreen.Hide();
        _gameScreen.Show();
    }

    private void OnPictureComplete(IPictureData picture)
    {
        _victoryScreen.Init(picture.Health);
        _victoryScreen.Show();
        _holderZone.Deactivated();
        _gameScreen.Hide();
    }

    private void OnPictureFail(IPictureData picture)
    {
        _lossScreen.Show();
        _holderZone.Deactivated();
    } 

    private void EndenAddCoinsEffects()
    {
        _victoryScreen.Hide();
        _holderZone.Activated();
        _startScreen.Show();
        _firstClickHandler.ResetClick();
        UpdatingUIForNewStart?.Invoke();
        VictoryScreenClosed?.Invoke();
        _walletDisplay.StopAnimationCoins();
    }

    private void OnShowedEmptyPicturePart(IPicturePartData picturePartData)
    {
        _gameScreen.UpdateScreen(picturePartData);
    }
}
