export interface Login {
    email: string;
    username: string;
    password: string;
}

export interface Register {
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    phoneNumber: string;
    dateOfBirth: string;
    gender: string;
    imageName?: string;
}

export interface UserProfile {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    phoneNumber: string;
    dateOfBirth: Date;
    gender: string;
    imageName: string;
    createdAt: string;
    createdBy: string;
    lastUpdatedAt: string;
    lastUpdatedBy: string;
}

export interface RecentChats {
    firstName: string,
    lastName: string,
    username: string,
    chatContent: string,
    senderId: number,
    recieverId: number,
    createdAt: Date,
}

export interface Chats {
    id: number,
    chatType: number,
    chatContent: string,
    createdAt: Date,
    updatedAt: Date,
    senderId: number,
    senderProfile: object | null,
    recieverId: number,
    recieverProfile: object | null,
    repliedTo: number
}

export interface SendChat {
    senderId: number,
    recieverId: number,
    chatType: number,
    chatContent: string,
    repliedTo?: number
}

export interface userSearchResult {
    id: number,
    username: string,
    firstName: string,
    lastName: string,
    imageName: string
}
