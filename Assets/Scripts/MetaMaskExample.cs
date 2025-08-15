using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BlockchainUnity.Managers;
using BlockchainUnity.Models;

/// <summary>
/// Example script demonstrating how to use the blockchain-for-unity plugin with MetaMask
/// This script provides a simple UI for connecting to MetaMask, getting wallet information,
/// and performing basic blockchain operations.
/// </summary>
public class MetaMaskExample : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button connectButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private Button getBalanceButton;
    
    [Header("Status Display")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text addressText;
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text networkText;
    
    [Header("Blockchain Manager")]
    [SerializeField] private BlockchainManager blockchainManager;

    private void OnEnable(){
        connectButton.onClick.AddListener(ConnectToMetaMask);
        getBalanceButton.onClick.AddListener(GetBalance);
        blockchainManager.OnWalletConnected += OnWalletConnected;
        blockchainManager.OnBalanceReceived += OnBalanceReceived;
    }

    private void OnDisable(){
        connectButton.onClick.RemoveListener(ConnectToMetaMask);
        getBalanceButton.onClick.RemoveListener(GetBalance);
        blockchainManager.OnWalletConnected -= OnWalletConnected;
        blockchainManager.OnBalanceReceived -= OnBalanceReceived;
    }

    private void ConnectToMetaMask(){
        blockchainManager.ConnectWallet();
    }

    private void OnWalletConnected(WalletConnectionResult result){
        statusText.text = "Connected to MetaMask";
        addressText.text = result.address;
        networkText.text = $"Network: {blockchainManager.GetCurrentNetwork().networkName}";
    }

    private void GetBalance(){
        blockchainManager.GetBalance();
    }

    private void OnBalanceReceived(BalanceResult result){
        balanceText.text = $"Balance: {result.formattedBalance} {result.currencySymbol}";
    }
}