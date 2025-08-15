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
    

    
    private void Start()
    {
        // Auto-find the manager if not assigned
        if (blockchainManager == null)
        {
            blockchainManager = FindObjectOfType<BlockchainManager>();
        }

        // Setup UI
        SetupUI();

        // Subscribe to events
        if (blockchainManager != null)
        {
            blockchainManager.OnWalletConnected += OnWalletConnected;
            blockchainManager.OnWalletDisconnected += OnWalletDisconnected;
            blockchainManager.OnBalanceReceived += OnBalanceReceived;
            blockchainManager.OnError += OnError;
            
            // Check if already connected
            if (blockchainManager.IsConnected)
            {
                UpdateStatus("Already Connected");
                UpdateAddress(blockchainManager.CurrentAddress);
                UpdateNetwork(blockchainManager.GetNetworkName());
                UpdateUIState(true);
            }
        }
        
        // Initialize UI state
        UpdateUIState(false);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from events
        if (blockchainManager != null)
        {
            blockchainManager.OnWalletConnected -= OnWalletConnected;
            blockchainManager.OnWalletDisconnected -= OnWalletDisconnected;
            blockchainManager.OnBalanceReceived -= OnBalanceReceived;
            blockchainManager.OnError -= OnError;
        }
    }
    
    private void Update()
    {
        // Test controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConnectToWallet();
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetBalance();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            DisconnectWallet();
        }

        // Show current network info with 'N' key
        if (Input.GetKeyDown(KeyCode.N))
        {
            ShowCurrentNetwork();
        }
        
        // Auto-check connection state and update UI if needed
        CheckAndUpdateConnectionState();
    }
    
    /// <summary>
    /// Setup UI button listeners
    /// </summary>
    private void SetupUI()
    {
        if (connectButton != null)
            connectButton.onClick.AddListener(OnConnectButtonClicked);
            
        if (disconnectButton != null)
            disconnectButton.onClick.AddListener(OnDisconnectButtonClicked);
            
        if (getBalanceButton != null)
            getBalanceButton.onClick.AddListener(OnGetBalanceButtonClicked);
    }
    

    
    /// <summary>
    /// Update UI state based on connection status
    /// </summary>
    private void UpdateUIState(bool isConnected)
    {
        if (connectButton != null)
            connectButton.interactable = !isConnected;
            
        if (disconnectButton != null)
            disconnectButton.interactable = isConnected;
            
        if (getBalanceButton != null)
            getBalanceButton.interactable = isConnected;
    }
    
    /// <summary>
    /// Handle Connect button click
    /// </summary>
    private void OnConnectButtonClicked()
    {
        UpdateStatus("Connecting to MetaMask...");
        blockchainManager?.ConnectWallet();
    }
    
    /// <summary>
    /// Handle Disconnect button click
    /// </summary>
    private void OnDisconnectButtonClicked()
    {
        UpdateStatus("Disconnected");
        blockchainManager?.DisconnectWallet();
    }
    
    /// <summary>
    /// Handle Get Balance button click
    /// </summary>
    private void OnGetBalanceButtonClicked()
    {
        UpdateStatus("Getting balance...");
        blockchainManager?.GetBalance();
    }
    
    /// <summary>
    /// Handle wallet connection success
    /// </summary>
    private void OnWalletConnected(WalletConnectionResult result)
    {
        if (result.success)
        {
            UpdateStatus("Connected to MetaMask");
            UpdateAddress(result.address);
            UpdateNetwork(blockchainManager.GetNetworkName());
            UpdateUIState(true);
            
            // Auto-get balance after successful connection
            GetBalance();
        }
        else
        {
            UpdateStatus($"Connection failed: {result.error}");
            UpdateUIState(false);
        }
    }
    
    /// <summary>
    /// Handle wallet disconnection
    /// </summary>
    private void OnWalletDisconnected(string message)
    {
        UpdateStatus("Disconnected");
        UpdateAddress("");
        UpdateBalance("");
        UpdateNetwork("");
        UpdateUIState(false);
    }
    
    /// <summary>
    /// Handle balance received
    /// </summary>
    private void OnBalanceReceived(BalanceResult result)
    {
        if (result.success)
        {
            UpdateBalance($"{result.formattedBalance} {result.currencySymbol}");
            UpdateStatus("Connected to MetaMask");
        }
        else
        {
            UpdateStatus($"Balance error: {result.error}");
        }
    }
    
    /// <summary>
    /// Handle errors
    /// </summary>
    private void OnError(string error)
    {
        UpdateStatus($"Error: {error}");
    }
    
    /// <summary>
    /// Update status text
    /// </summary>
    private void UpdateStatus(string status)
    {
        if (statusText != null)
            statusText.text = $"Status: {status}";
    }
    
    /// <summary>
    /// Update address text
    /// </summary>
    private void UpdateAddress(string address)
    {
        if (addressText != null)
            addressText.text = $"Address: {address}";
    }
    
    /// <summary>
    /// Update balance text
    /// </summary>
    private void UpdateBalance(string balance)
    {
        if (balanceText != null)
            balanceText.text = $"Balance: {balance}";
    }
    
    /// <summary>
    /// Update network text
    /// </summary>
    private void UpdateNetwork(string network)
    {
        if (networkText != null)
            networkText.text = $"Network: {network}";
    }
    
    /// <summary>
    /// Public method to get current connection status
    /// </summary>
    public bool IsConnected()
    {
        return blockchainManager != null && blockchainManager.IsConnected;
    }
    
    /// <summary>
    /// Public method to get current wallet address
    /// </summary>
    public string GetCurrentAddress()
    {
        return blockchainManager?.CurrentAddress ?? "";
    }
    
    /// <summary>
    /// Public method to get current network name
    /// </summary>
    public string GetCurrentNetwork()
    {
        return blockchainManager?.GetNetworkName() ?? "";
    }
    
    /// <summary>
    /// Public method to manually refresh balance
    /// </summary>
    public void RefreshBalance()
    {
        OnGetBalanceButtonClicked();
    }
    
    public void ConnectToWallet()
    {
        UpdateStatus("Connecting to MetaMask...");
        blockchainManager?.ConnectWallet();
    }

    public void GetBalance()
    {
        UpdateStatus("Getting balance...");
        
        // If we're connected but UI isn't updated, force update it
        if (blockchainManager != null && blockchainManager.IsConnected)
        {
            bool shouldUpdateUI = !disconnectButton.interactable || !getBalanceButton.interactable;
            if (shouldUpdateUI)
            {
                ForceUpdateUIState();
            }
        }
        
        // Use custom RPC request to get balance and handle the response manually
        if (blockchainManager != null && blockchainManager.IsConnected)
        {
            var request = new RpcRequest
            {
                method = "eth_getBalance",
                @params = new string[] { blockchainManager.CurrentAddress, "latest" },
                id = UnityEngine.Random.Range(1, 1000)
            };
            
            blockchainManager.SendCustomRpcRequest(request, 
                (response) => {
                    ParseAndDisplayBalance(response.result);
                },
                (error) => {
                    UpdateStatus($"Balance error: {error}");
                }
            );
        }
        else
        {
            blockchainManager?.GetBalance();
        }
    }

    public void DisconnectWallet()
    {
        UpdateStatus("Disconnected");
        blockchainManager?.DisconnectWallet();
    }
    
    /// <summary>
    /// Show current network info
    /// </summary>
    [ContextMenu("Show Network Info")]
    public void ShowCurrentNetwork()
    {
        var currentNetwork = blockchainManager?.GetCurrentNetwork();
        if (currentNetwork != null)
        {
            UpdateStatus($"Network: {currentNetwork.DisplayName}");
        }
    }
    

    
    /// <summary>
    /// Force update UI state based on current connection
    /// </summary>
    private void ForceUpdateUIState()
    {
        if (blockchainManager != null)
        {
            bool isConnected = blockchainManager.IsConnected;
            string address = blockchainManager.CurrentAddress;
            string network = blockchainManager.GetNetworkName();
            
            UpdateUIState(isConnected);
            
            if (isConnected && !string.IsNullOrEmpty(address))
            {
                UpdateStatus("Connected to MetaMask");
                UpdateAddress(address);
                UpdateNetwork(network);
            }
            else
            {
                UpdateStatus("Disconnected");
                UpdateAddress("");
                UpdateNetwork("");
            }
        }
    }
    
    /// <summary>
    /// Automatically parse and display balance from hex value
    /// </summary>
    private void ParseAndDisplayBalance(string hexBalance)
    {
        try
        {
            if (string.IsNullOrEmpty(hexBalance))
                return;
            
            // Remove "0x" prefix if present
            string hexWithoutPrefix = hexBalance.StartsWith("0x") ? hexBalance.Substring(2) : hexBalance;
            
            // Convert to decimal (wei)
            System.Numerics.BigInteger wei = System.Numerics.BigInteger.Parse("0" + hexWithoutPrefix, System.Globalization.NumberStyles.HexNumber);
            
            // Convert wei to ETH (divide by 10^18)
            decimal eth = (decimal)wei / (decimal)System.Math.Pow(10, 18);
            
            // Format to 6 decimal places and remove trailing zeros
            string formatted = eth.ToString("F6").TrimEnd('0').TrimEnd('.');
            if (formatted == "") formatted = "0";
            
            UpdateBalance($"{formatted} SepoliaETH");
            UpdateStatus("Connected to MetaMask");
        }
        catch (System.Exception e)
        {
            UpdateBalance("Error parsing balance");
        }
    }
    
    /// <summary>
    /// Check connection state and update UI automatically
    /// </summary>
    private void CheckAndUpdateConnectionState()
    {
        if (blockchainManager != null)
        {
            bool isConnected = blockchainManager.IsConnected;
            string currentAddress = blockchainManager.CurrentAddress;
            
            // Check if UI state doesn't match actual connection state
            bool uiConnected = disconnectButton != null && disconnectButton.interactable;
            
            if (isConnected && !uiConnected)
            {
                UpdateStatus("Connected to MetaMask");
                UpdateAddress(currentAddress);
                UpdateNetwork(blockchainManager.GetNetworkName());
                UpdateUIState(true);
                
                // Auto-get balance
                GetBalance();
            }
            else if (!isConnected && uiConnected)
            {
                UpdateStatus("Disconnected");
                UpdateAddress("");
                UpdateBalance("");
                UpdateNetwork("");
                UpdateUIState(false);
            }
        }
    }
    

}
