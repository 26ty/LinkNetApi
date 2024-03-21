# LinkNetApi
## LinkNetWeb的API
#### 社群平台留言板主題資料表設計ER-Model:
![](https://hackmd.io/_uploads/Byw8cTx23.png)
1. User(使用者)表格

欄位名稱 | 資料類型 | 說明
--- | --- | ---
id | INTEGER | 使用者ID，主鍵
username | VARCHAR(50) | 使用者名稱，唯一
password | VARCHAR(100) | 使用者密碼
email | VARCHAR(50) | 使用者電子郵件，唯一
avatar | VARCHAR(255) | 使用者頭像圖片路徑
created_at | TIMESTAMP | 使用者建立時間
updated_at | TIMESTAMP | 使用者最近更新時間

2. Article(文章)表格

欄位名稱 | 資料類型 | 說明
--- | --- | ---
id | VARCHAR2(80) | 文章ID，主鍵
title | VARCHAR2(80) | 文章標題
content | CLOB | 文章內容
img_url | varchar1(80) | 文章圖片
user_id | INTEGER | 作者ID，外鍵參考User表格的id欄位
created_at | VARCHAR2(80) | 文章建立時間
updated_at | TIMESTAMP | 文章最近更新時間

3. Comment(回覆)表格

欄位名稱 | 資料類型 | 說明
--- | --- | ---
id | INTEGER | 回覆ID，主鍵
content | TEXT | 回覆內容
user_id | INTEGER | 回覆者ID，外鍵參考User表格的id欄位
article_id | INTEGER | 回覆對應的文章ID，外鍵參考Article表格的id欄位
created_at | TIMESTAMP | 回覆建立時間
updated_at | TIMESTAMP | 回覆最近更新時間


![image](https://github.com/26ty/LinkNetApi/assets/69799370/b1b4f780-8765-4a2e-9717-90604005e271)
![image](https://github.com/26ty/LinkNetApi/assets/69799370/9003aa3c-5d14-44db-9cde-d530a09c8930)
![image](https://github.com/26ty/LinkNetApi/assets/69799370/c17a6355-62e5-4fa2-9e83-4e9b9a01c8ab)
![image](https://github.com/26ty/LinkNetApi/assets/69799370/1b9c84af-8920-44b1-b569-8e6afaeb6eab)


