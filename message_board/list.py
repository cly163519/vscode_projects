messages = []

def show_messages():
    if not messages:
        print("No messages yet.")
        return
    for i, msg in enumerate(messages):
        print(f"{i+1}. {msg['name']}: {msg['content']}")

def add_message():
    name = input("Your name: ")
    content = input("Your message: ")
    messages.append({"name": name, "content": content})
    print("Message posted!")

# main loop
while True:
    print("\n--- Message Board ---")
    print("1. View messages")
    print("2. Post message")
    print("3. Exit")
    
    choice = input("Choose: ")
    
    if choice == "1":
        show_messages()
    elif choice == "2":
        add_message()
    elif choice == "3":
        break
    else:
        print("Invalid choice")