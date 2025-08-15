# Unity Blockchain SDK Examples

This repository contains examples demonstrating how to integrate blockchain functionality into Unity applications using the blockchain-for-unity plugin.

## 🚀 Features

- **MetaMask Integration**: Connect to MetaMask wallet from Unity WebGL builds
- **Wallet Management**: Connect, disconnect, and manage wallet connections
- **Balance Checking**: Get and display wallet balances
- **Network Support**: Support for multiple Ethereum networks (Sepolia testnet by default)
- **Automatic UI Updates**: Real-time UI updates based on connection state

## 📋 Prerequisites

- Unity 2022.3 LTS or newer
- MetaMask browser extension installed and unlocked
- WebGL build target (MetaMask only works in web browsers)
- Infura account (for RPC endpoint)

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/tomicz/unity-blockchain-sdk-examples.git
cd unity-blockchain-sdk-examples
```

### 2. Open in Unity

- Open Unity Hub
- Add the project folder
- Open the project in Unity

### 3. Configure Network Settings

The project uses a configuration file for network settings. **Important**: Never commit your actual API keys!

1. **Create your config.json** (if not exists):

   ```json
   {
     "networkName": "sepolia",
     "chainId": 11155111,
     "rpcUrl": "https://sepolia.infura.io/v3/YOUR_PROJECT_ID",
     "currencySymbol": "SepoliaETH",
     "isTestnet": true
   }
   ```

2. **Replace `YOUR_PROJECT_ID`** with your actual Infura project ID
3. **The config.json file is in .gitignore** to prevent API key leaks

### 4. Build for WebGL

- Set build target to WebGL
- Build and run in a web browser
- Make sure MetaMask is installed and unlocked

## 🎮 Examples

### MetaMaskExample

A complete example showing how to integrate MetaMask with Unity UI.

#### Features:

- **Connect Button**: Connect to MetaMask wallet
- **Disconnect Button**: Disconnect from wallet
- **Get Balance Button**: Fetch and display wallet balance
- **Status Display**: Real-time connection status
- **Address Display**: Show connected wallet address
- **Balance Display**: Show current wallet balance
- **Network Display**: Show current network

#### Setup:

1. Attach `MetaMaskExample.cs` to a GameObject in your scene
2. Assign UI references in the Inspector:
   - **Connect Button**: Button for connecting to MetaMask
   - **Disconnect Button**: Button for disconnecting
   - **Get Balance Button**: Button for fetching balance
   - **Status Text**: TMP_Text for displaying status
   - **Address Text**: TMP_Text for displaying wallet address
   - **Balance Text**: TMP_Text for displaying balance
   - **Network Text**: TMP_Text for displaying network
   - **Blockchain Manager**: (Optional) Assign or let it auto-find

#### Usage:

1. **Build for WebGL** and run in browser
2. **Click Connect** to connect to MetaMask
3. **Approve connection** in MetaMask popup
4. **Address and balance** will automatically load
5. **Use Get Balance** to refresh balance
6. **Click Disconnect** to disconnect

#### Keyboard Shortcuts:

- **Space**: Connect to MetaMask
- **B**: Get Balance
- **D**: Disconnect
- **N**: Show Network Info

## 🔧 Configuration

### Network Configuration

The project supports multiple Ethereum networks. Update `config.json` for different networks:

#### Sepolia Testnet (Default)

```json
{
  "networkName": "sepolia",
  "chainId": 11155111,
  "rpcUrl": "https://sepolia.infura.io/v3/YOUR_PROJECT_ID",
  "currencySymbol": "SepoliaETH",
  "isTestnet": true
}
```

#### Ethereum Mainnet

```json
{
  "networkName": "ethereum",
  "chainId": 1,
  "rpcUrl": "https://mainnet.infura.io/v3/YOUR_PROJECT_ID",
  "currencySymbol": "ETH",
  "isTestnet": false
}
```

### RPC Providers

You can use different RPC providers:

- **Infura**: `https://sepolia.infura.io/v3/YOUR_PROJECT_ID`
- **Alchemy**: `https://eth-sepolia.g.alchemy.com/v2/YOUR_API_KEY`
- **QuickNode**: `https://your-endpoint.quiknode.pro/YOUR_API_KEY/`

## 🔒 Security Best Practices

1. **Never commit API keys** - config.json is in .gitignore
2. **Use environment variables** for production builds
3. **Regenerate API keys** if accidentally exposed
4. **Use testnet** for development and testing
5. **Validate user inputs** before sending transactions

## 🐛 Troubleshooting

### Common Issues:

#### "MetaMask not found"

- Ensure MetaMask is installed and unlocked
- Make sure you're running in a web browser (WebGL build)
- Check if MetaMask is on the correct network

#### "Connection failed"

- Verify your RPC URL is correct
- Check if your Infura project is active
- Ensure MetaMask is on the same network as your config

#### "Balance not showing"

- Check if the wallet has funds on the selected network
- Verify the RPC endpoint is responding
- Check browser console for errors

#### "Buttons not working"

- Ensure all UI elements are assigned in the Inspector
- Check if the BlockchainManager is properly initialized
- Verify the script is attached to an active GameObject

### Debug Information:

- Check browser console for detailed error messages
- Use keyboard shortcuts for testing functionality
- Verify network configuration in config.json

## 📚 API Reference

### MetaMaskExample Methods:

- `ConnectToWallet()`: Connect to MetaMask
- `DisconnectWallet()`: Disconnect from wallet
- `GetBalance()`: Fetch wallet balance
- `IsConnected()`: Check connection status
- `GetCurrentAddress()`: Get current wallet address
- `GetCurrentNetwork()`: Get current network name

### Events:

- `OnWalletConnected`: Fired when wallet connects
- `OnWalletDisconnected`: Fired when wallet disconnects
- `OnBalanceReceived`: Fired when balance is received
- `OnError`: Fired when an error occurs

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## ⚠️ Disclaimer

This is example code for educational purposes. Always test thoroughly before using in production. Never use real funds for testing without proper security measures.

## 🔗 Links

- [Blockchain for Unity Plugin](https://github.com/tomicz/blockchain-for-unity)
- [MetaMask Documentation](https://docs.metamask.io/)
- [Infura Documentation](https://docs.infura.io/)
- [Ethereum Documentation](https://ethereum.org/developers/)
