﻿CREATE TABLE "Blogs" (
    "BlogId" INTEGER NOT NULL CONSTRAINT "PK_Blogs" PRIMARY KEY AUTOINCREMENT,
    "Url" TEXT NOT NULL
);


CREATE TABLE "Posts" (
    "PostId" INTEGER NOT NULL CONSTRAINT "PK_Posts" PRIMARY KEY AUTOINCREMENT,
    "NewColumn2" INTEGER NOT NULL,
    "Title" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "BlogId" INTEGER NOT NULL,
    CONSTRAINT "FK_Posts_Blogs_BlogId" FOREIGN KEY ("BlogId") REFERENCES "Blogs" ("BlogId") ON DELETE CASCADE
);


CREATE INDEX "IX_Posts_BlogId" ON "Posts" ("BlogId");


