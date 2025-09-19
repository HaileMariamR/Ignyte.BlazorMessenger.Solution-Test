# Blazor Chat UI Library

This Razor Class Library provides a ready-to-use Chat UI built with Blazor. It includes reusable components for chat rooms, messages, and inputs, along with a flexible design system powered by CSS variables.

The library is fully customizable: developers can easily override default CSS variables or add their own styles without modifying the core library.

# 📦 Components
## 1. ChatRoomPanel

The main container for the chat UI.
```
<ChatRoomPanel 
    ChatRoom="@chatRoom"
    CurrentUser="@currentUser"
    OnMessageSent="HandleMessageSent"
    OnMessageDeleted="HandleMessageDeleted" />
```

Parameters:

- ChatRoom? chatRoom → The current chat room with messages.

- User? currentUser → The logged-in user.

- EventCallback<Message> OnMessageSent → Triggered when a message is sent.

- EventCallback<Message> OnMessageDeleted → Triggered when a message is deleted.

## 2. ChatMessage

Displays a single chat message.
```
<ChatMessage 
    message="@message"
    OnMessageDeletedCallback="HandleMessageDeleted" />
```

Features:

- Shows sender avatar, name, and timestamp.

- Supports scheduled messages with a removable "chip".

## 3. ChatInput

Input area for writing and sending messages.
```
<ChatInput OnSend="HandleSendMessage" />
```

Features:

- Multiline text area for typing messages.

- Optional datetime picker for scheduling messages.

- "Send" button.

Parameters:

- EventCallback<Message> OnSend → Triggered when a new message is submitted.

# 🎨 Styling & Customization

The UI is styled using CSS variables defined under :root.
Developers can override these variables globally (in their Blazor app’s wwwroot/css/site.css) without touching the library source.

Example: Override Primary Color
```
:root {
  --color-primary: #ff5722; /* Change to orange */
  --color-secondary: #e64a19;
}
```
# 📐 CSS Design Tokens
## 🎨 Colors
```
--color-primary: #4361ee;
--color-secondary: #3f37c9;
--color-success: #4cc9f0;
--color-background: #f8f9fa;
--color-surface: #ffffff;
--color-text-primary: #212529;
--color-text-secondary: #6c757d;
--color-border: #dee2e6;
--color-shadow: rgba(0, 0, 0, 0.1);
--color-message-own-bg: #eef2ff;
--color-code-bg: #2d2d2d;
--color-code-text: #f8f8f2;
--color-chip-close: var(--color-text-secondary);
--color-datetime-border: #ccc;
```
## ✍ Typography
```
--font-family-base: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
--font-size-base: 1rem;
--font-size-sm: 0.875rem;
--font-size-xs: 0.75rem;
--font-size-code: 0.875rem;
--font-size-chip: 0.6rem;
--font-weight-normal: 400;
--font-weight-medium: 500;
--font-weight-bold: 700;
```
## 📏 Spacing
```
--space-xs: 4px;
--space-sm: 8px;
--space-md: 16px;
--space-lg: 24px;
--space-xl: 32px;
```
## 📐 Dimensions
```
--chat-room-height: 600px;
--chat-room-width: 800px;
--message-width: 50%;
--input-height: 60px;
--avatar-size: 32px;
--demo-max-width: 1000px;
--datetime-width: 150px;
--datetime-height: 40px;
```

## 🔲 Borders & Radius
```
--border-default: 1px solid var(--color-border);
--radius-sm: 3px;
--radius-md: 8px;
--radius-lg: 16px;
--radius-full: 50%;
```

## 🌑 Shadows
```
--shadow-default: 0 2px 10px var(--color-shadow);
```

## 📦 Padding Presets
```
--message-padding: var(--space-sm) var(--space-md);
--input-padding: var(--space-sm) var(--space-sm);
--chip-padding: 2px 10px;
--datetime-padding: 2px 4px;
--code-padding: var(--space-md);
```

## 🛠 How to Customize Styles

You can modify the UI by overriding CSS variables or applying your own styles.

1. Override Variables Globally

Add the overrides to your site.css or any global stylesheet:
```
:root {
  --chat-room-width: 100%;
  --chat-room-height: 700px;
  --avatar-size: 40px;
  --color-message-own-bg: #d1ffd6;
}
```
2. Use Component-Specific Overrides
```
.chat-room-container {
  border-radius: 0; /* Remove rounded corners */
}

```
# 🚀 Quick Start

Add the Razor Class Library to your solution.
```
<ProjectReference Include="..\Ignyte.BlazorMessenger.UI\Ignyte.BlazorMessenger.UI.csproj" />
```

Reference the CSS in your Blazor WebAssembly or Server app:
```
<link href="_content/Ignyte.BlazorMessenger.UI/css/chat.css" rel="stylesheet" />

```
Use Components in your .razor pages:
```
<ChatRoomPanel 
    ChatRoom="@room" 
    CurrentUser="@currentUser"
    OnMessageSent="MessageSent"
    OnMessageDeleted="MessageDeleted" />
```
# 📌 Notes

By default, messages from the current user are aligned to the right and styled with --color-message-own-bg.

Scheduled messages are displayed with a chip and can be deleted before sending.

The library is designed to be extended, users can wrap or replace components as needed.
